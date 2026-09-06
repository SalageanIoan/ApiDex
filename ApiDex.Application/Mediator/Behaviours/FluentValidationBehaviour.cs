using ApiDex.Domain.Results;
using FluentValidation;
using MediatR;

namespace ApiDex.Application.Mediator.Behaviours;

public sealed class FluentValidationBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validatorList = validators.ToList();
        if (validatorList.Count == 0)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validatorList.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        if (failures.Count == 0)
        {
            return await next();
        }

        var message = string.Join("; ", failures.Select(failure => failure.ErrorMessage));
        var error = Error.Validation("Validation.Failed", message);

        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(error);
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var failure = typeof(TResponse)
                .GetMethod(nameof(Result.Failure), [typeof(Error)])!
                .Invoke(null, [error])!;

            return (TResponse)failure;
        }

        throw new ValidationException(failures);
    }
}

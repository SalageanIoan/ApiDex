using ApiDex.Domain.Results;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Results;

public static class MinimalResultExtensions
{
    public static IResult ToMinimalResult(this Result result, Func<IResult> onSuccess)
    {
        return result.IsSuccess ? onSuccess() : ToErrorResult(result.Error!);
    }

    public static IResult ToMinimalResult<T>(this Result<T> result, Func<T, IResult> onSuccess)
    {
        return result.IsSuccess ? onSuccess(result.Value) : ToErrorResult(result.Error!);
    }

    private static IResult ToErrorResult(Error error)
    {
        var body = new { error.Code, error.Message };

        return error.Type switch
        {
            ErrorType.NotFound => HttpResults.NotFound(body),
            ErrorType.Conflict => HttpResults.Conflict(body),
            ErrorType.Validation => HttpResults.BadRequest(body),
            _ => HttpResults.Problem(
                error.Message,
                statusCode: StatusCodes.Status500InternalServerError
            ),
        };
    }
}

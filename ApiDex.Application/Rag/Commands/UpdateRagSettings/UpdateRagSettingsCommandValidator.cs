using ApiDex.Domain.Rag;
using FluentValidation;

namespace ApiDex.Application.Rag.Commands.UpdateRagSettings;

public sealed class UpdateRagSettingsCommandValidator : AbstractValidator<UpdateRagSettingsCommand>
{
    public UpdateRagSettingsCommandValidator()
    {
        RuleFor(request => request.Message.ActiveModel)
            .Must(value => string.IsNullOrWhiteSpace(value) ||
                           Enum.TryParse<EmbeddingModelType>(value, true, out _));

        RuleFor(request => request.Message.ContextMode)
            .Must(value => string.IsNullOrWhiteSpace(value) ||
                           Enum.TryParse<RagContextMode>(value, true, out _));
    }
}

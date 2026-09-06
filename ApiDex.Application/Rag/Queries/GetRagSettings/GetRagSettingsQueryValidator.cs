using FluentValidation;

namespace ApiDex.Application.Rag.Queries.GetRagSettings;

public sealed class GetRagSettingsQueryValidator : AbstractValidator<GetRagSettingsQuery>
{
    public GetRagSettingsQueryValidator()
    {
    }
}

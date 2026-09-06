using ApiDex.Application.Rag.Dtos;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Queries.GetRagSettings;

public sealed class GetRagSettingsQueryHandler(IRagSettingsManager settingsManager)
    : IRequestHandler<GetRagSettingsQuery, RagSettingsOut>
{
    public async Task<RagSettingsOut> Handle(GetRagSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = await settingsManager.GetSettingsAsync(cancellationToken);
        var modelDescriptors = EmbeddingModelDescriptors.All
            .Select(model => new RagModelDescriptorOut(
                model.Type.ToString(),
                model.Key,
                model.Dimensions,
                model.Type != EmbeddingModelType.OpenAi))
            .ToList();

        return new RagSettingsOut(
            settings.ActiveModel.Type.ToString(),
            settings.ContextMode.ToString(),
            Enum.GetNames<EmbeddingModelType>(),
            Enum.GetNames<RagContextMode>(),
            modelDescriptors);
    }
}

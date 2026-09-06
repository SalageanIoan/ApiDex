using ApiDex.Application.Rag.Dtos;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Commands.UpdateRagSettings;

public sealed class UpdateRagSettingsCommandHandler(
    IRagSettingsManager settingsManager,
    IVectorStoreRepository vectorStoreRepository) : IRequestHandler<UpdateRagSettingsCommand, UpdateRagSettingsResult>
{
    public async Task<UpdateRagSettingsResult> Handle(UpdateRagSettingsCommand request,
        CancellationToken cancellationToken)
    {
        var currentSettings = await settingsManager.GetSettingsAsync(cancellationToken);
        var activeModel = currentSettings.ActiveModel.Type;
        var contextMode = currentSettings.ContextMode;

        if (!string.IsNullOrWhiteSpace(request.Message.ActiveModel) &&
            !Enum.TryParse(request.Message.ActiveModel, true, out activeModel))
        {
            return UpdateRagSettingsResult.Failure("Invalid model type");
        }

        if (!string.IsNullOrWhiteSpace(request.Message.ContextMode) &&
            !Enum.TryParse(request.Message.ContextMode, true, out contextMode))
        {
            return UpdateRagSettingsResult.Failure("Invalid context mode");
        }

        await settingsManager.UpdateSettingsAsync(activeModel, contextMode, cancellationToken);
        await vectorStoreRepository.EnsureCollectionAsync(cancellationToken);

        return UpdateRagSettingsResult.Success();
    }
}

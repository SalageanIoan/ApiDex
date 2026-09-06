namespace ApiDex.Application.Rag.Dtos;

public sealed record UpdateRagSettingsResult(bool IsSuccess, string? Error)
{
    public static UpdateRagSettingsResult Success()
    {
        return new UpdateRagSettingsResult(true, null);
    }

    public static UpdateRagSettingsResult Failure(string error)
    {
        return new UpdateRagSettingsResult(false, error);
    }
}

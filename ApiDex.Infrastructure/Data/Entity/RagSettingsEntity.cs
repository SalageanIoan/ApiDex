namespace ApiDex.Infrastructure.Data.Entity;

public class RagSettingsEntity
{
    public string Id { get; set; } = string.Empty;

    public string ActiveModel { get; set; } = string.Empty;

    public string ContextMode { get; set; } = string.Empty;

    public int TopKChunks { get; set; }
}

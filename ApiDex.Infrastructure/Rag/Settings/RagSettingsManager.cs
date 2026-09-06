using ApiDex.Domain.Rag;
using ApiDex.Infrastructure.Data;

namespace ApiDex.Infrastructure.Rag.Settings;

public partial class RagSettingsManager(ApiDexDbContext dbContext) : IRagSettingsManager
{
    private const string DefaultSettingsId = "default";
}

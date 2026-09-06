namespace ApiDex.Domain.Rag;

public static class RagContextDefaults
{
    public static int GetTopKChunks(RagContextMode contextMode)
    {
        return contextMode switch
        {
            RagContextMode.Expanded => 8,
            RagContextMode.Comprehensive => 12,
            _ => 5
        };
    }
}

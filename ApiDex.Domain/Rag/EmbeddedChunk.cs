namespace ApiDex.Domain.Rag;

public class EmbeddedChunk
{
    public required string Id { get; init; }

    public required string Content { get; init; }

    public required float[] Embedding { get; init; }

    public required Guid ProjectId { get; init; }

    public Guid? ServiceId { get; init; }

    public Guid? EndpointId { get; init; }

    public required string Source { get; init; }
}

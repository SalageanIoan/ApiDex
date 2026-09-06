namespace ApiDex.Domain.Rag;

public class DocumentationChunk
{
    public required string Id { get; init; }

    public required string Content { get; init; }

    public required Guid ProjectId { get; init; }

    public Guid? ServiceId { get; init; }

    public Guid? EndpointId { get; init; }

    public required string Source { get; init; }
}

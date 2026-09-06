using System.Security.Cryptography;
using System.Text;
using ApiDex.Domain.Rag;
using Qdrant.Client.Grpc;
using static Qdrant.Client.Grpc.Conditions;

namespace ApiDex.Infrastructure.Rag.Vector.Qdrant;

public partial class QdrantVectorStoreRepository
{
    private static Dictionary<string, Value> CreatePayload(EmbeddedChunk chunk, EmbeddingModelDescriptor activeModel)
    {
        var payload = new Dictionary<string, Value>
        {
            ["chunkId"] = chunk.Id,
            ["content"] = chunk.Content,
            ["projectId"] = chunk.ProjectId.ToString(),
            ["source"] = chunk.Source,
            ["embeddingModel"] = activeModel.Key
        };

        if (chunk.ServiceId is not null)
        {
            payload["serviceId"] = chunk.ServiceId.Value.ToString();
        }

        if (chunk.EndpointId is not null)
        {
            payload["endpointId"] = chunk.EndpointId.Value.ToString();
        }

        return payload;
    }

    private static Filter? CreateFilter(Guid? projectId, Guid? serviceId)
    {
        var conditions = new List<Condition>();

        if (projectId is not null)
        {
            conditions.Add(MatchKeyword("projectId", projectId.Value.ToString()));
        }

        if (serviceId is not null)
        {
            conditions.Add(MatchKeyword("serviceId", serviceId.Value.ToString()));
        }

        return conditions.Count == 0
            ? null
            : new Filter { Must = { conditions } };
    }

    private static Filter CreateProjectFilter(Guid projectId)
    {
        return new Filter
        {
            Must = { MatchKeyword("projectId", projectId.ToString()) }
        };
    }

    private static RetrievedChunk? ToRetrievedChunk(ScoredPoint point)
    {
        if (point.Payload.Count == 0) return null;

        return new RetrievedChunk
        {
            Id = GetPayloadString(point.Payload, "chunkId") ?? point.Id.ToString(),
            Content = GetPayloadString(point.Payload, "content") ?? string.Empty,
            Score = point.Score,
            ProjectId = Guid.Parse(GetPayloadString(point.Payload, "projectId")!),
            ServiceId = TryGetGuid(point.Payload, "serviceId"),
            EndpointId = TryGetGuid(point.Payload, "endpointId"),
            Source = GetPayloadString(point.Payload, "source") ?? string.Empty
        };
    }

    private static Guid? TryGetGuid(IReadOnlyDictionary<string, Value> payload, string key)
    {
        var value = GetPayloadString(payload, key);
        return Guid.TryParse(value, out var guid) ? guid : null;
    }

    private static string? GetPayloadString(IReadOnlyDictionary<string, Value> payload, string key)
    {
        return payload.TryGetValue(key, out var value) ? value.StringValue : null;
    }

    private static Guid ToPointId(string chunkId)
    {
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(chunkId));
        hash[6] = (byte)((hash[6] & 0x0F) | 0x30);
        hash[8] = (byte)((hash[8] & 0x3F) | 0x80);
        return new Guid(hash);
    }
}

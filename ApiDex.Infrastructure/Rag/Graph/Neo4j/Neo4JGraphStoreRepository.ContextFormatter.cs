using System.Text;
using Neo4j.Driver;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    private static string BuildContext(IRecord record)
    {
        var builder = new StringBuilder();
        AppendMapList(builder, "Graph services",
            record["services"].As<List<Dictionary<string, object>>>());
        AppendMapList(builder, "Graph anchor endpoints",
            record["anchorEndpoints"].As<List<Dictionary<string, object>>>());
        AppendMapList(builder, "Graph dependencies", record["dependencies"].As<List<Dictionary<string, object>>>());
        AppendMapList(builder, "Graph events", record["events"].As<List<Dictionary<string, object>>>());
        AppendMapList(builder, "Graph incoming calls", GetOptionalList(record, "incomingCalls"));
        AppendMapList(builder, "Graph outgoing calls", GetOptionalList(record, "outgoingCalls"));
        return builder.ToString();
    }

    private static List<Dictionary<string, object>> GetOptionalList(IRecord record, string key)
    {
        return record.Keys.Contains(key) ? record[key].As<List<Dictionary<string, object>>>() : [];
    }

    private static void AppendMapList(StringBuilder builder, string title,
        IReadOnlyList<Dictionary<string, object>> values)
    {
        var lines = values
            .Select(ToLine)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Distinct()
            .ToList();

        if (lines.Count == 0)
        {
            return;
        }

        builder.AppendLine(title + ":");
        foreach (var line in lines)
        {
            builder.AppendLine("- " + line);
        }
    }

    private static string ToLine(Dictionary<string, object> values)
    {
        return string.Join("; ", values
            .Where(_ => true)
            .Select(pair => $"{pair.Key}: {pair.Value}"));
    }
}

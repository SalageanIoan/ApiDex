using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Queries.AskDocumentation;

public record AskDocumentationQuery(string Question, Guid? ProjectId = null, Guid? ServiceId = null)
    : IRequest<AskDocumentationResult>;

public sealed record AskDocumentationResult(string Answer, ChatTokenUsageMetadata? Usage);

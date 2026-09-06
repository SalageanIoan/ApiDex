using ApiDex.Application.Rag.Dtos;
using MediatR;

namespace ApiDex.Application.Rag.Queries.GetRagSettings;

public sealed record GetRagSettingsQuery : IRequest<RagSettingsOut>
{
    public static GetRagSettingsQuery Create()
    {
        return new GetRagSettingsQuery();
    }
}

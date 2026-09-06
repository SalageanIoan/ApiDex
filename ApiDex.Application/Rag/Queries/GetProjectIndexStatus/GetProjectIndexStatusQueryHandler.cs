using ApiDex.Application.Rag.Dtos;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Queries.GetProjectIndexStatus;

public sealed class GetProjectIndexStatusQueryHandler(IVectorStoreRepository vectorStoreRepository)
    : IRequestHandler<GetProjectIndexStatusQuery, ProjectIndexStatusOut>
{
    public async Task<ProjectIndexStatusOut> Handle(GetProjectIndexStatusQuery request,
        CancellationToken cancellationToken)
    {
        var activeCount = await vectorStoreRepository.CountByProjectIdAsync(request.ProjectId, false,
            cancellationToken);
        var allModelsCount = await vectorStoreRepository.CountByProjectIdAsync(request.ProjectId, true,
            cancellationToken);

        return new ProjectIndexStatusOut(activeCount, allModelsCount);
    }
}

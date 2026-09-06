using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Rag;
using MediatR;

namespace ApiDex.Application.Rag.Commands.ClearProjectIndex;

public class ClearProjectIndexCommandHandler(
    IDocumentationProjectRepository projectRepository,
    IVectorStoreRepository vectorStoreRepository,
    IGraphStoreRepository graphStoreRepository) : IRequestHandler<ClearProjectIndexCommand, bool>
{
    public async Task<bool> Handle(ClearProjectIndexCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetDocumentationProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return false;
        }

        await vectorStoreRepository.DeleteByProjectIdAsync(request.ProjectId, true, cancellationToken);
        await graphStoreRepository.DeleteProjectAsync(request.ProjectId, cancellationToken);
        return true;
    }
}

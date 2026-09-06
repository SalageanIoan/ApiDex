using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Rag;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteDocumentationProject;

public class DeleteDocumentationProjectHandler(
    IDocumentationProjectRepository documentationProjectRepository,
    IVectorStoreRepository vectorStoreRepository,
    IGraphStoreRepository graphStoreRepository)
    : CommandHandler<DeleteDocumentationProjectRequest>
{
    public override async Task<Result> Handle(DeleteDocumentationProjectRequest command,
        CancellationToken cancellationToken)
    {
        var deleted = await documentationProjectRepository.DeleteDocumentationProjectAsync(
            command.DocumentationProjectId,
            cancellationToken);

        if (!deleted)
        {
            return Error.NotFound(
                "DocumentationProject.NotFound",
                $"Documentation project '{command.DocumentationProjectId}' was not found.");
        }

        await vectorStoreRepository.DeleteByProjectIdAsync(
            command.DocumentationProjectId,
            allModels: true,
            cancellationToken);
        await graphStoreRepository.DeleteProjectAsync(command.DocumentationProjectId, cancellationToken);

        return Result.Success();
    }
}

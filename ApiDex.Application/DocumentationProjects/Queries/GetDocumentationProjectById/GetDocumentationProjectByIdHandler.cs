using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjectById;

public class GetDocumentationProjectByIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetDocumentationProjectByIdRequest, Result<DocumentationProjectOut>>
{
    public async Task<Result<DocumentationProjectOut>> Handle(GetDocumentationProjectByIdRequest request,
        CancellationToken cancellationToken)
    {
        var project = await documentationProjectRepository.GetDocumentationProjectByIdAsync(
            request.DocumentationProjectId,
            cancellationToken);

        if (project is null)
        {
            return Error.NotFound(
                "DocumentationProject.NotFound",
                $"Documentation project '{request.DocumentationProjectId}' was not found.");
        }

        return new DocumentationProjectOut
        {
            Id = project.Id,
            Title = project.Title,
            GeneralDescription = project.GeneralDescription,
            ArchitectureType = project.ArchitectureType,
            Services = project.Services.Select(s => new ServiceDocumentationOut
            {
                Id = s.Id,
                Title = s.Title,
                GeneralDescription = s.GeneralDescription
            }).ToList()
        };
    }
}
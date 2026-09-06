using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjects;

public class GetDocumentationProjectsHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetDocumentationProjectsRequest, Result<List<DocumentationProjectSummaryOut>>>
{
    public async Task<Result<List<DocumentationProjectSummaryOut>>> Handle(GetDocumentationProjectsRequest request,
        CancellationToken cancellationToken)
    {
        var projects = await documentationProjectRepository.GetDocumentationProjectsAsync(cancellationToken);

        var result = projects.Select(project => new DocumentationProjectSummaryOut
        {
            Id = project.Id,
            Title = project.Title,
            GeneralDescription = project.GeneralDescription,
            ArchitectureType = project.ArchitectureType
        }).ToList();

        return result;
    }
}

using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjects;

public class GetDocumentationProjectsRequest : IRequest<Result<List<DocumentationProjectSummaryOut>>>
{
    public static GetDocumentationProjectsRequest Create()
    {
        return new GetDocumentationProjectsRequest();
    }
}
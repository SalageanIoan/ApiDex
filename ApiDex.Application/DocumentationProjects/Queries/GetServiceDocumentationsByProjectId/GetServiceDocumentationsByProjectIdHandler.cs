using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationsByProjectId;

public class GetServiceDocumentationsByProjectIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceDocumentationsByProjectIdRequest, Result<List<ServiceDocumentationOut>>>
{
    public async Task<Result<List<ServiceDocumentationOut>>> Handle(
        GetServiceDocumentationsByProjectIdRequest request,
        CancellationToken cancellationToken)
    {
        var services = await documentationProjectRepository.GetServiceDocumentationsByProjectIdAsync(
            request.DocumentationProjectId,
            cancellationToken);

        var result = services.Select(service => new ServiceDocumentationOut
        {
            Id = service.Id,
            Title = service.Title,
            GeneralDescription = service.GeneralDescription
        }).ToList();

        return result;
    }
}
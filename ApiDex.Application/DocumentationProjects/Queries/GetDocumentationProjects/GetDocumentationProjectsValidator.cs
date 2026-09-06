using FluentValidation;

namespace ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjects;

public class GetDocumentationProjectsValidator : AbstractValidator<GetDocumentationProjectsRequest>
{
	public GetDocumentationProjectsValidator()
	{
	}
}
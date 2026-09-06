using MediatR;

namespace ApiDex.Application.Rag.Commands.IndexProject;

public record IndexProjectCommand(Guid ProjectId) : IRequest<bool>;

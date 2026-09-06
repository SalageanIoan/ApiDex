using MediatR;

namespace ApiDex.Application.Rag.Commands.ClearProjectIndex;

public record ClearProjectIndexCommand(Guid ProjectId) : IRequest<bool>;

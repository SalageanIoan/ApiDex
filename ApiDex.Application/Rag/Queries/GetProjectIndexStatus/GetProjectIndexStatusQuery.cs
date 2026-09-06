using ApiDex.Application.Rag.Dtos;
using MediatR;

namespace ApiDex.Application.Rag.Queries.GetProjectIndexStatus;

public sealed record GetProjectIndexStatusQuery(Guid ProjectId) : IRequest<ProjectIndexStatusOut>;

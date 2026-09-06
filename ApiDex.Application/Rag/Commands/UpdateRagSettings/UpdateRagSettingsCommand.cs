using ApiDex.Application.Rag.Dtos;
using MediatR;

namespace ApiDex.Application.Rag.Commands.UpdateRagSettings;

public sealed record UpdateRagSettingsCommand(UpdateRagSettingsIn Message) : IRequest<UpdateRagSettingsResult>;

using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Robots.Dtos;

namespace WebApp.UrlShortener.Contracts.Robots.Commands;

public sealed record AddInstructionCommand(string Id, string Instruction) : IRequest<Result<AddInstructionResponseDto, InvalidOperationException>>;

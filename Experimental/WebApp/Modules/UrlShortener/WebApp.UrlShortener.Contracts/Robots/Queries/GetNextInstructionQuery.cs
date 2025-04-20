using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Robots.Dtos;

namespace WebApp.UrlShortener.Contracts.Robots.Queries;

public sealed record GetNextInstructionQuery(string Id) : IRequest<Result<GetNextInstructionResponseDto, InvalidOperationException>>;

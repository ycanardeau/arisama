using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Urls.Dtos;

namespace WebApp.UrlShortener.Contracts.Urls.Queries;

public sealed record GoQuery(string ShortenedRouteSegment) : IRequest<Result<GoResponseDto, InvalidOperationException>>;

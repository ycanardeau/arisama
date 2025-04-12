using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Urls.Dtos;

namespace WebApp.UrlShortener.Contracts.Urls.Queries;

public sealed record ShortenQuery(string Url) : IRequest<Result<ShortenResponseDto, InvalidOperationException>>;

using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Urls.Dtos;
using WebApp.UrlShortener.Contracts.Urls.Queries;

namespace WebApp.UrlShortener.Endpoints.Urls;

internal class ShortenEndpoint(ISender sender) : Endpoint<ShortenQuery, ShortenResponseDto>
{
	public override void Configure()
	{
		Get("/shorten");
		AllowAnonymous();
		Group<UrlsGroup>();
		Description(builder => builder
			.Produces<ShortenResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(ShortenQuery req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

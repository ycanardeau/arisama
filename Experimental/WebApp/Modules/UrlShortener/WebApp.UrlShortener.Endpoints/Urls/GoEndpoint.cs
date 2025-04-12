using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Urls.Dtos;
using WebApp.UrlShortener.Contracts.Urls.Queries;

namespace WebApp.UrlShortener.Endpoints.Urls;

internal class GoEndpoint(ISender sender) : Endpoint<GoQuery, GoResponseDto>
{
	public override void Configure()
	{
		Get("/go");
		AllowAnonymous();
		Group<UrlsGroup>();
		Description(builder => builder
			.Produces<GoResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(GoQuery req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

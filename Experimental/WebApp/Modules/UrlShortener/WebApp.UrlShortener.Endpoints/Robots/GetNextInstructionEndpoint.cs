using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Robots.Dtos;
using WebApp.UrlShortener.Contracts.Robots.Queries;

namespace WebApp.UrlShortener.Endpoints.Robots;

internal class GetNextInstructionEndpoint(ISender sender) : Endpoint<GetNextInstructionQuery, GetNextInstructionResponseDto>
{
	public override void Configure()
	{
		Get("/{id}/instructions");
		AllowAnonymous();
		Group<RobotsGroup>();
		Description(builder => builder
			.Produces<GetNextInstructionResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(GetNextInstructionQuery req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

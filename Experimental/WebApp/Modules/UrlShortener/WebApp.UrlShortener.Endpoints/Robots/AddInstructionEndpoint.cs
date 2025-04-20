using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Robots.Commands;
using WebApp.UrlShortener.Contracts.Robots.Dtos;

namespace WebApp.UrlShortener.Endpoints.Robots;

internal class AddInstructionEndpoint(ISender sender) : Endpoint<AddInstructionCommand, AddInstructionResponseDto>
{
	public override void Configure()
	{
		Post("/{id}/instructions");
		AllowAnonymous();
		Group<RobotsGroup>();
		Description(builder => builder
			.Produces<AddInstructionResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(AddInstructionCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

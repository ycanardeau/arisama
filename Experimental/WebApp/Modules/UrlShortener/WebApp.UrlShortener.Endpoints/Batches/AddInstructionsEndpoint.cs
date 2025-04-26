using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Batches.Commands;
using WebApp.UrlShortener.Contracts.Batches.Dtos;

namespace WebApp.UrlShortener.Endpoints.Batches;

internal class AddInstructionsEndpoint(ISender sender) : Endpoint<AddInstructionsCommand, AddInstructionsResponseDto>
{
	public override void Configure()
	{
		Post("/");
		AllowAnonymous();
		Group<BatchesGroup>();
		Description(builder => builder
			.Produces<AddInstructionsResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(AddInstructionsCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

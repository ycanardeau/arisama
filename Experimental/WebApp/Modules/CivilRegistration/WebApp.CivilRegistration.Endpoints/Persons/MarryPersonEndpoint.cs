using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class MarryPersonEndpoint(ISender sender) : Endpoint<MarryPersonCommand, MarryPersonResponseDto>
{
	public override void Configure()
	{
		Put("/{id}/marry");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Accepts<MarryPersonCommand>()
			.Produces<MarryPersonResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(MarryPersonCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class WidowPersonEndpoint(ISender sender) : Endpoint<WidowPersonCommand, WidowPersonResponseDto>
{
	public override void Configure()
	{
		Put("/{id}/widow");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Accepts<WidowPersonCommand>()
			.Produces<WidowPersonResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(WidowPersonCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

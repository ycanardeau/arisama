using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class CreatePersonEndpoint(ISender sender) : Endpoint<CreatePersonCommand, CreatePersonResponseDto>
{
	public override void Configure()
	{
		Post("/");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Produces<CreatePersonResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(CreatePersonCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

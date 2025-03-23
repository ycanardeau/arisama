using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class CreatePersonEndpoint(ISender sender) : EndpointWithoutRequest<CreatePersonResponseDto>
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

	public override Task HandleAsync(CancellationToken ct)
	{
		return sender.Send(new CreatePersonCommand(), ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

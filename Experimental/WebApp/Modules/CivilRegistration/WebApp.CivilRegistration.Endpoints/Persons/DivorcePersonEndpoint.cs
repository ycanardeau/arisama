using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class DivorcePersonEndpoint(ISender sender) : Endpoint<DivorcePersonCommand, DivorcePersonResponseDto>
{
	public override void Configure()
	{
		Put("/{id}/divorce");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Accepts<DivorcePersonCommand>()
			.Produces<DivorcePersonResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(DivorcePersonCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

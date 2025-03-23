using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class GetPersonEndpoint(ISender sender) : Endpoint<GetPersonQuery, GetPersonResponseDto>
{
	public override void Configure()
	{
		Get("/{id}");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Produces<GetPersonResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(GetPersonQuery req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

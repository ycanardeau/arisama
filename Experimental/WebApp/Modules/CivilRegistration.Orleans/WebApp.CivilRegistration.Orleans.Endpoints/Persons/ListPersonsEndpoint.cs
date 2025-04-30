using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Endpoints.Persons;

internal class ListPersonsEndpoint(ISender sender) : EndpointWithoutRequest<ListPersonsResponseDto>
{
	public override void Configure()
	{
		Get("/");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Produces<ListPersonsResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(CancellationToken ct)
	{
		return sender.Send(new ListPersonsQuery(), ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

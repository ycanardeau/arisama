using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class GetCurrentMaritalStatusEndpoint(ISender sender) : Endpoint<GetCurrentMaritalStatusQuery, GetCurrentMaritalStatusResponseDto>
{
	public override void Configure()
	{
		Get("/{id}/marital-status");
		AllowAnonymous();
		Group<PersonsGroup>();
		Description(builder => builder
			.Produces<GetCurrentMaritalStatusResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(GetCurrentMaritalStatusQuery req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

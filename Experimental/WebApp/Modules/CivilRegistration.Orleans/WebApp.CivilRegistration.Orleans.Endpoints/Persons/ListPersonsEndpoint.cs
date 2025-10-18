using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Endpoints.Persons;

public class ListPersonsEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - Civil Registration - People")]
	[HttpGet("/orleans/civil-registration/people")]
	[AllowAnonymous]
	[Produces<ListPersonsResponseDto>]
	public async Task<IResult> HandleAsync(CancellationToken ct)
	{
		var response = await sender.Send(new ListPersonsQuery(), ct);
		return response.ToMinimalApiResult();
	}
}

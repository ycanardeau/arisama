using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Endpoints.Persons;

public class ListPersonsEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - People")]
	[HttpGet("/civil-registration/people")]
	[AllowAnonymous]
	[Produces<ListPersonsResponseDto>]
	public async Task<IResult> HandleAsync(CancellationToken ct)
	{
		var response = await sender.Send(new ListPersonsQuery(), ct);
		return response.ToMinimalApiResult();
	}
}

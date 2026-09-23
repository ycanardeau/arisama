using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Orleans.Endpoints.People;

public class ListPeopleEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - Civil Registration - People")]
	[HttpGet("/orleans/civil-registration/people")]
	[AllowAnonymous]
	[Produces<ListPeopleResponseDto>]
	public async Task<IResult> HandleAsync(CancellationToken ct)
	{
		var response = await sender.Send(new ListPeopleQuery(), ct);
		return response.ToMinimalApiResult();
	}
}

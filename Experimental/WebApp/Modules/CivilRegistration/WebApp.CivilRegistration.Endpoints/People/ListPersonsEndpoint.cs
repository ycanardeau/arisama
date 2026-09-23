using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Endpoints.People;

public class ListPeopleEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - People")]
	[HttpGet("/civil-registration/people")]
	[AllowAnonymous]
	[Produces<ListPeopleResponseDto>]
	public async Task<IResult> HandleAsync(CancellationToken ct)
	{
		var response = await sender.Send(new ListPeopleQuery(), ct);
		return response.ToMinimalApiResult();
	}
}

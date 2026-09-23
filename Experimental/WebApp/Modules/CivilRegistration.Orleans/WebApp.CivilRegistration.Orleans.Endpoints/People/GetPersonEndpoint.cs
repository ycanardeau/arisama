using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Orleans.Endpoints.People;

public class GetPersonEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - Civil Registration - People")]
	[HttpGet("/orleans/civil-registration/people/{id}")]
	[AllowAnonymous]
	[Produces<GetPersonResponseDto>]
	public async Task<IResult> HandleAsync(Guid id, GetPersonQuery req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

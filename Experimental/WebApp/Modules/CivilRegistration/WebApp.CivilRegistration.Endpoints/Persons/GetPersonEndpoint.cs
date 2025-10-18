using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Endpoints.Persons;

public class GetPersonEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - People")]
	[HttpGet("/civil-registration/people/{id}")]
	[AllowAnonymous]
	[Produces<GetPersonResponseDto>]
	public async Task<IResult> HandleAsync(Guid id, CancellationToken ct)
	{
		var response = await sender.Send(new GetPersonQuery(id), ct);
		return response.ToMinimalApiResult();
	}
}

using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Endpoints.Persons;

public class GetCurrentMaritalStatusEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - People")]
	[HttpGet("/civil-registration/people/{id}/marital-status")]
	[AllowAnonymous]
	[Produces<GetCurrentMaritalStatusResponseDto>]
	public async Task<IResult> HandleAsync(Guid id, CancellationToken ct)
	{
		var response = await sender.Send(new GetCurrentMaritalStatusQuery(id), ct);
		return response.ToMinimalApiResult();
	}
}

using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Endpoints.Persons;

public class GetCurrentMaritalStatusEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - Civil Registration - People")]
	[HttpGet("/orleans/civil-registration/people/{id}/marital-status")]
	[AllowAnonymous]
	[Produces<GetCurrentMaritalStatusResponseDto>]
	public async Task<IResult> HandleAsync(Guid id, GetCurrentMaritalStatusQuery req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Orleans.Endpoints.Persons;

public class CreatePersonEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - Civil Registration - People")]
	[HttpPost("/orleans/civil-registration/people")]
	[AllowAnonymous]
	[Produces<CreatePersonResponseDto>]
	public async Task<IResult> HandleAsync(CreatePersonCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

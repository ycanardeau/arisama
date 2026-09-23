using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.People.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Orleans.Endpoints.People;

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

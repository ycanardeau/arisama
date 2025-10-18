using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Endpoints.Persons;

public class CreatePersonEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - People")]
	[HttpPost("/civil-registration/people")]
	[AllowAnonymous]
	[Produces<CreatePersonResponseDto>]
	public async Task<IResult> HandleAsync(CreatePersonCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

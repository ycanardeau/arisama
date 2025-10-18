using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;

namespace WebApp.CivilRegistration.Endpoints.DeathCertificates;

public class CreateDeathCertificateEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - Death Certificates")]
	[HttpPost("/civil-registration/death-certificates")]
	[AllowAnonymous]
	[Produces<CreateDeathCertificateResponseDto>]
	public async Task<IResult> HandleAsync(CreateDeathCertificateCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

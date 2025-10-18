using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;

namespace WebApp.CivilRegistration.Endpoints.DivorceCertificates;

public class CreateDivorceCertificateEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - Divorce Certificates")]
	[HttpPost("/civil-registration/divorce-certificates")]
	[AllowAnonymous]
	[Produces<CreateDivorceCertificateResponseDto>]
	public async Task<IResult> HandleAsync(CreateDivorceCertificateCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

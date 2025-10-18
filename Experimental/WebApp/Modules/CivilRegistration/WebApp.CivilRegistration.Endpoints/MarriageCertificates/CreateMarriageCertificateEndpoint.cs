using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Endpoints.MarriageCertificates;

public class CreateMarriageCertificateEndpoint(ISender sender) : ControllerBase
{
	[Tags("Civil Registration - Marriage Certificates")]
	[HttpPost("/civil-registration/marriage-certificates")]
	[AllowAnonymous]
	[Produces<CreateMarriageCertificateResponseDto>]
	public async Task<IResult> HandleAsync(CreateMarriageCertificateCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

using Microsoft.AspNetCore.Authorization;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Endpoints.MarriageCertificates;

public class CreateMarriageCertificateEndpoint(ISender sender) : ControllerBase
{
	[Tags("Orleans - CivilRegistration - Marriage Certificates")]
	[HttpPost("/orleans/civil-registration/marriage-certificates")]
	[AllowAnonymous]
	[Produces<CreateMarriageCertificateResponseDto>]
	public async Task<IResult> HandleAsync(CreateMarriageCertificateCommand req, CancellationToken ct)
	{
		var response = await sender.Send(req, ct);
		return response.ToMinimalApiResult();
	}
}

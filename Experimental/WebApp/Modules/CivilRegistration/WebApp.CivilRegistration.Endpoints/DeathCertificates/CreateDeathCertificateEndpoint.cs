using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;

namespace WebApp.CivilRegistration.Endpoints.DeathCertificates;

internal class CreateDeathCertificateEndpoint(ISender sender) : Endpoint<CreateDeathCertificateCommand, CreateDeathCertificateResponseDto>
{
	public override void Configure()
	{
		Post("/");
		AllowAnonymous();
		Group<DeathCertificatesGroup>();
		Description(builder => builder
			.Produces<CreateDeathCertificateResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(CreateDeathCertificateCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

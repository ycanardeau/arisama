using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;

namespace WebApp.CivilRegistration.Endpoints.DivorceCertificates;

internal class CreateDivorceCertificateEndpoint(ISender sender) : Endpoint<CreateDivorceCertificateCommand, CreateDivorceCertificateResponseDto>
{
	public override void Configure()
	{
		Post("/");
		AllowAnonymous();
		Group<DivorceCertificatesGroup>();
		Description(builder => builder
			.Produces<CreateDivorceCertificateResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(CreateDivorceCertificateCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

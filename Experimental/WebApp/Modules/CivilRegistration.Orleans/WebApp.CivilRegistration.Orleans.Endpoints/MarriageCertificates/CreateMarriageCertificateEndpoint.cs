using DiscriminatedOnions;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Endpoints.MarriageCertificates;

internal class CreateMarriageCertificateEndpoint(ISender sender) : Endpoint<CreateMarriageCertificateCommand, CreateMarriageCertificateResponseDto>
{
	public override void Configure()
	{
		Post("/");
		AllowAnonymous();
		Group<MarriageCertificatesGroup>();
		Description(builder => builder
			.Produces<CreateMarriageCertificateResponseDto>()
			.ProducesProblemFE()
		);
	}

	public override Task HandleAsync(CreateMarriageCertificateCommand req, CancellationToken ct)
	{
		return sender.Send(req, ct)
			.Pipe(ResultExtensions.ToApiResult)
			.Pipe(SendResultAsync);
	}
}

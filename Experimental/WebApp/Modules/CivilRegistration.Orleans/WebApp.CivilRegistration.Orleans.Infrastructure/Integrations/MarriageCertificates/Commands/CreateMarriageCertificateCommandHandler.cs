using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.MarriageCertificates.Commands;

internal class CreateMarriageCertificateCommandHandler() : IRequestHandler<CreateMarriageCertificateCommand, Result<CreateMarriageCertificateResponseDto>>
{
	public Task<Result<CreateMarriageCertificateResponseDto>> Handle(CreateMarriageCertificateCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

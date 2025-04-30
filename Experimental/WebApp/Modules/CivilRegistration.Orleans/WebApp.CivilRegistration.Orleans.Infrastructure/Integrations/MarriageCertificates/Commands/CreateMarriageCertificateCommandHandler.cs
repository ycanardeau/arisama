using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.MarriageCertificates.Commands;

internal class CreateMarriageCertificateCommandHandler() : IRequestHandler<CreateMarriageCertificateCommand, Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>
{
	public Task<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>> Handle(CreateMarriageCertificateCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

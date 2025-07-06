using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.MarriageCertificates.Commands;

internal class CreateMarriageCertificateCommandHandler(IGrainFactory grains) : IRequestHandler<CreateMarriageCertificateCommand, Result<CreateMarriageCertificateResponseDto>>
{
	public Task<Result<CreateMarriageCertificateResponseDto>> Handle(CreateMarriageCertificateCommand request, CancellationToken cancellationToken)
	{
		var husbandGrain = grains.GetGrain<IPersonGrain>(request.HusbandId);

		var wifeGrain = grains.GetGrain<IPersonGrain>(request.WifeId);

		var marriageCertificateGrain = grains.GetGrain<IMarriageCertificateGrain>(Guid.CreateVersion7());

		return marriageCertificateGrain.Marry(husbandGrain, wifeGrain)
			.FlatMap(() => Result.Ok(new CreateMarriageCertificateResponseDto(Id: marriageCertificateGrain.GetPrimaryKey())));
	}
}

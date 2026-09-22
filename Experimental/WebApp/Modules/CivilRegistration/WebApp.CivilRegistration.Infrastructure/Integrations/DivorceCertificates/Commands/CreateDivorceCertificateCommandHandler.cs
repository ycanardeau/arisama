using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;
using WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.DivorceCertificates.Commands;

internal class CreateDivorceCertificateCommandHandler(ApplicationDbContext dbContext)
	: IRequestHandler<
		CreateDivorceCertificateCommand,
		Result<CreateDivorceCertificateResponseDto, CivilRegistrationError>
	>
{
	public async Task<Result<CreateDivorceCertificateResponseDto, CivilRegistrationError>> Handle(
		CreateDivorceCertificateCommand request,
		CancellationToken cancellationToken
	)
	{
		var marriageCertificate = await dbContext
			.MarriageCertificates.Include(x => x.Husband.MaritalStateMachine)
			.Include(x => x.Wife.MaritalStateMachine)
			.SingleOrDefaultAsync(
				x => x.Id == new MarriageCertificateId(request.MarriageCertificateId),
				cancellationToken
			);

		if (marriageCertificate is null)
		{
			return new CivilRegistrationError.MarriageCertificateNotFound();
		}

		return await DivorceCertificate
			.Create(new CreateCommand(marriageCertificate))
			.Tap(x => dbContext.DivorceCertificates.Add(x))
			.Tap(x => dbContext.SaveChangesAsync(cancellationToken))
			.Map(x => new CreateDivorceCertificateResponseDto(Id: x.Id.Value));
	}
}

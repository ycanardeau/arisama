using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;
using WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.DivorceCertificates.Commands;

internal class CreateDivorceCertificateCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateDivorceCertificateCommand, Result<CreateDivorceCertificateResponseDto, InvalidOperationException>>
{
	public async Task<Result<CreateDivorceCertificateResponseDto, InvalidOperationException>> Handle(CreateDivorceCertificateCommand request, CancellationToken cancellationToken)
	{
		var marriageCertificate = await dbContext.MarriageCertificates
			.Include(x => x.Husband.MaritalStateMachine.States)
			.Include(x => x.Wife.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new MarriageCertificateId(request.MarriageCertificateId), cancellationToken);

		if (marriageCertificate is null)
		{
			return Result.Error(new InvalidOperationException());
		}

		return await DivorceCertificate.Create(new CreateCommand(marriageCertificate))
			.MapAsync(async x =>
			{
				dbContext.DivorceCertificates.Add(x);

				await dbContext.SaveChangesAsync(cancellationToken);

				return new CreateDivorceCertificateResponseDto(Id: x.Id.Value);
			});
	}
}

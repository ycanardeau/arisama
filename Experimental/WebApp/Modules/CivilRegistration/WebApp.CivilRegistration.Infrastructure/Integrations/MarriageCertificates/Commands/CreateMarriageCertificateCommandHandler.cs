using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.MarriageCertificates.Commands;

internal class CreateMarriageCertificateCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateMarriageCertificateCommand, Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>
{
	public async Task<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>> Handle(CreateMarriageCertificateCommand request, CancellationToken cancellationToken)
	{
		var husband = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.HusbandId), cancellationToken);

		if (husband is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.HusbandId} not found"));
		}

		var wife = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.WifeId), cancellationToken);

		if (wife is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.WifeId} not found"));
		}

		return await MarriageCertificate.Create(new CreateCommand(husband, wife))
			.MapAsync(async x =>
			{
				dbContext.MarriageCertificates.Add(x);

				await dbContext.SaveChangesAsync(cancellationToken);

				return new CreateMarriageCertificateResponseDto(Id: x.Id.Value);
			});
	}
}

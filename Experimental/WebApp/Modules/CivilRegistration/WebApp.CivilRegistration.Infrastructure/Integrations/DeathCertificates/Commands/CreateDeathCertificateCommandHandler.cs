using MediatR;
using Microsoft.EntityFrameworkCore;
using Nut.Results;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;
using WebApp.CivilRegistration.Domain.DeathCertificates.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.DeathCertificates.Commands;

internal class CreateDeathCertificateCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateDeathCertificateCommand, Result<CreateDeathCertificateResponseDto>>
{
	public async Task<Result<CreateDeathCertificateResponseDto>> Handle(CreateDeathCertificateCommand request, CancellationToken cancellationToken)
	{
		var deceased = await dbContext.Persons
			.Include(x => x.MaritalStateMachine)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.DeceasedId), cancellationToken);

		if (deceased is null)
		{
			return Result.Error<CreateDeathCertificateResponseDto>(new InvalidOperationException($"Person {request.DeceasedId} not found"));
		}

		var widowed = deceased.MaritalStateMachine.CurrentState is not Married state
			? null
			: await dbContext.Persons
				.Include(x => x.MaritalStateMachine)
				.SingleAsync(x => x.Id == state.MarriageInformation.MarriedWithId, cancellationToken);

		return await DeathCertificate.Create(new CreateCommand(Deceased: deceased, Widowed: widowed))
			.Tap(x => dbContext.DeathCertificates.Add(x))
			.Tap(x => dbContext.SaveChangesAsync(cancellationToken))
			.Map(x => new CreateDeathCertificateResponseDto(Id: x.Id.Value));
	}
}

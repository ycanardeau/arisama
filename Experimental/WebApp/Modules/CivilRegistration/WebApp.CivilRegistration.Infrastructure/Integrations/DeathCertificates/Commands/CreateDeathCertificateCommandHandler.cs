using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;
using WebApp.CivilRegistration.Domain.DeathCertificates.Entities;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.DeathCertificates.Commands;

internal class CreateDeathCertificateCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateDeathCertificateCommand, Result<CreateDeathCertificateResponseDto, InvalidOperationException>>
{
	public async Task<Result<CreateDeathCertificateResponseDto, InvalidOperationException>> Handle(CreateDeathCertificateCommand request, CancellationToken cancellationToken)
	{
		var deceased = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.DeceasedId), cancellationToken);

		if (deceased is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.DeceasedId} not found"));
		}

		var widowed = deceased.MaritalStateMachine.CurrentState is not MarriedState state
			? null
			: await dbContext.Persons
				.Include(x => x.MaritalStateMachine.States)
				.SingleAsync(x => x.Id == state.Payload.MarriageInformation.MarriedWithId, cancellationToken);

		return await DeathCertificate.Create(new CreateCommand(Deceased: deceased, Widowed: widowed))
			.MapAsync(async x =>
			{
				dbContext.DeathCertificates.Add(x);

				await dbContext.SaveChangesAsync(cancellationToken);

				return new CreateDeathCertificateResponseDto(Id: x.Id.Value);
			});
	}
}

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
		var person1 = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Person1Id), cancellationToken);

		if (person1 is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Person1Id} not found"));
		}

		var person2 = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Person2Id), cancellationToken);

		if (person2 is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Person2Id} not found"));
		}

		return await MarriageCertificate.Create(new CreateCommand(person1, person2))
			.MapAsync(async x =>
			{
				dbContext.MarriageCertificates.Add(x);

				await dbContext.SaveChangesAsync(cancellationToken);

				return new CreateMarriageCertificateResponseDto(Id: x.Id.Value);
			});
	}
}

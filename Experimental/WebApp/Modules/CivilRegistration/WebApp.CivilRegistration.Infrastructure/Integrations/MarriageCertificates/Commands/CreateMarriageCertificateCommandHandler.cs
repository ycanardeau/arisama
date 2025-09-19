using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.MarriageCertificates.Commands;

internal class CreateMarriageCertificateCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateMarriageCertificateCommand, Result<CreateMarriageCertificateResponseDto>>
{
	private async Task<Result<Person>> GetPersonAsync(PersonId personId, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine)
			.SingleOrDefaultAsync(x => x.Id == personId, cancellationToken);

		return person is null
			? Result.Error<Person>(new InvalidOperationException($"Person {personId} not found"))
			: person;
	}

	public Task<Result<CreateMarriageCertificateResponseDto>> Handle(CreateMarriageCertificateCommand request, CancellationToken cancellationToken)
	{
		return GetPersonAsync(new PersonId(request.HusbandId), cancellationToken)
			.Combine(x => GetPersonAsync(new PersonId(request.WifeId), cancellationToken))
			.FlatMap(x => MarriageCertificate.Create(new CreateCommand(x.Item1, x.Item2)))
			.Tap(x => dbContext.MarriageCertificates.Add(x))
			.Tap(x => dbContext.SaveChangesAsync(cancellationToken))
			.Map(x => new CreateMarriageCertificateResponseDto(Id: x.Id.Value));
	}
}

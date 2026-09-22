using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetPersonQueryHandler(ApplicationDbContext dbContext, IPersonMapper personMapper)
	: IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto, CivilRegistrationError>>
{
	private async Task<Result<Person, CivilRegistrationError>> GetPerson(
		GetPersonQuery request,
		CancellationToken cancellationToken
	)
	{
		var person = await dbContext
			.Persons.Include(x => x.MaritalStateMachine)
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return new CivilRegistrationError.PersonNotFound();
		}

		return person;
	}

	public Task<Result<GetPersonResponseDto, CivilRegistrationError>> Handle(
		GetPersonQuery request,
		CancellationToken cancellationToken
	)
	{
		return GetPerson(request, cancellationToken)
			.Map(x => new GetPersonResponseDto(Person: personMapper.Map(x)));
	}
}

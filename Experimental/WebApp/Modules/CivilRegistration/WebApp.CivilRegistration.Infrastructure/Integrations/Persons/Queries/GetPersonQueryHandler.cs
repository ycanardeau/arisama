using MediatR;
using Microsoft.EntityFrameworkCore;
using Nut.Results;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetPersonQueryHandler(
	ApplicationDbContext dbContext,
	IPersonMapper personMapper
) : IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto>>
{
	private async Task<Result<Person>> GetPerson(GetPersonQuery request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error<Person>(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return person;
	}

	public Task<Result<GetPersonResponseDto>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		return GetPerson(request, cancellationToken)
			.Map(x => new GetPersonResponseDto(Person: personMapper.Map(x)));
	}
}

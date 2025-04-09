using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
) : IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto, InvalidOperationException>>
{
	private async Task<Result<Person, InvalidOperationException>> GetPerson(GetPersonQuery request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return Result.Ok(person);
	}

	public Task<Result<GetPersonResponseDto, InvalidOperationException>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		return GetPerson(request, cancellationToken)
			.Pipe(x => x.Map(x => new GetPersonResponseDto(Person: personMapper.Map(x))));
	}
}

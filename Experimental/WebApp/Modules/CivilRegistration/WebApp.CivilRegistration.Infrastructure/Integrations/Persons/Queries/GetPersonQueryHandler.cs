using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetPersonQueryHandler(
	ApplicationDbContext dbContext,
	IMaritalStatusMapper maritalStatusMapper
) : IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto, InvalidOperationException>>
{
	public async Task<Result<GetPersonResponseDto, InvalidOperationException>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return Result.Ok(new GetPersonResponseDto(new MaritalStateMachineDto(
			Version: person.MaritalStateMachine.Version.Value,
			States: [
				.. person.MaritalStateMachine.States
					.OrderBy(x => x.Version)
					.Select(maritalStatusMapper.Map)
			]
		)));
	}
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nut.Results;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class ListPersonsQueryHandler(
	ApplicationDbContext dbContext,
	IPersonMapper personMapper
) : IRequestHandler<ListPersonsQuery, Result<ListPersonsResponseDto>>
{
	public async Task<Result<ListPersonsResponseDto>> Handle(ListPersonsQuery request, CancellationToken cancellationToken)
	{
		var persons = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.ToListAsync(cancellationToken);

		return new ListPersonsResponseDto(Persons: [.. persons.Select(personMapper.Map)]);
	}
}

using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Contracts.People.Queries;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.People.Queries;

internal class ListPeopleQueryHandler(ApplicationDbContext dbContext, IPersonMapper personMapper)
	: IRequestHandler<ListPeopleQuery, Result<ListPeopleResponseDto, CivilRegistrationError>>
{
	public async Task<Result<ListPeopleResponseDto, CivilRegistrationError>> Handle(
		ListPeopleQuery request,
		CancellationToken cancellationToken
	)
	{
		var people = await dbContext
			.People.Include(x => x.MaritalStateMachine)
			.ToListAsync(cancellationToken);

		return new ListPeopleResponseDto(People: [.. people.Select(personMapper.Map)]);
	}
}

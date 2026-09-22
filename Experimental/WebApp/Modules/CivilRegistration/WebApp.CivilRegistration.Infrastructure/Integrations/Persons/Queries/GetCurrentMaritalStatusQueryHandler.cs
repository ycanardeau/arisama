using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetCurrentMaritalStatusQueryHandler(
	ApplicationDbContext dbContext,
	IMaritalStatusMapper maritalStatusMapper
)
	: IRequestHandler<
		GetCurrentMaritalStatusQuery,
		Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationError>
	>
{
	public async Task<Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationError>> Handle(
		GetCurrentMaritalStatusQuery request,
		CancellationToken cancellationToken
	)
	{
		var person = await dbContext
			.Persons.Include(x => x.MaritalStateMachine)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return new CivilRegistrationError.PersonNotFound();
		}

		return new GetCurrentMaritalStatusResponseDto(
			MaritalStatus: maritalStatusMapper.Map(person.MaritalStateMachine.CurrentState)
		);
	}
}

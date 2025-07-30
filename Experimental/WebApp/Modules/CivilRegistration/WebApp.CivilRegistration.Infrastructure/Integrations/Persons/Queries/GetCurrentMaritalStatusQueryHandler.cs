using MediatR;
using Microsoft.EntityFrameworkCore;
using Nut.Results;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetCurrentMaritalStatusQueryHandler(
	ApplicationDbContext dbContext,
	IMaritalStatusMapper maritalStatusMapper
) : IRequestHandler<GetCurrentMaritalStatusQuery, Result<GetCurrentMaritalStatusResponseDto>>
{
	public async Task<Result<GetCurrentMaritalStatusResponseDto>> Handle(GetCurrentMaritalStatusQuery request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error<GetCurrentMaritalStatusResponseDto>(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return new GetCurrentMaritalStatusResponseDto(
			MaritalStatus: maritalStatusMapper.Map(person.MaritalStateMachine.CurrentState)
		);
	}
}

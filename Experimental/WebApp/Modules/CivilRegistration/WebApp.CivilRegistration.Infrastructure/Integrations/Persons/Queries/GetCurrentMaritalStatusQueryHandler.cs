using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Queries;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Queries;

internal class GetCurrentMaritalStatusEndpoint(
	ApplicationDbContext dbContext,
	IMaritalStatusMapper maritalStatusMapper
) : IRequestHandler<GetCurrentMaritalStatusQuery, Result<GetCurrentMaritalStatusResponseDto, InvalidOperationException>>
{
	public async Task<Result<GetCurrentMaritalStatusResponseDto, InvalidOperationException>> Handle(GetCurrentMaritalStatusQuery request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return Result.Ok(new GetCurrentMaritalStatusResponseDto(
			MaritalStatus: maritalStatusMapper.Map(person.MaritalStateMachine.CurrentState)
		));
	}
}

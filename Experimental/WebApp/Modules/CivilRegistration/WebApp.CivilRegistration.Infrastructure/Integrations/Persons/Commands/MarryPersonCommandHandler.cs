using DiscriminatedOnions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Commands;

internal class MarryPersonCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<MarryPersonCommand, Result<MarryPersonResponseDto, InvalidOperationException>>
{
	public async Task<Result<MarryPersonResponseDto, InvalidOperationException>> Handle(MarryPersonCommand request, CancellationToken cancellationToken)
	{
		var person = await dbContext.Persons
			.Include(x => x.MaritalStateMachine.States)
			.SingleOrDefaultAsync(x => x.Id == new PersonId(request.Id), cancellationToken);

		if (person is null)
		{
			return Result.Error(new InvalidOperationException($"Person {request.Id} not found"));
		}

		return await person.Marry(new MarryCommand())
			.MapAsync(async x =>
			{
				await dbContext.SaveChangesAsync(cancellationToken);

				return new MarryPersonResponseDto();
			});
	}
}

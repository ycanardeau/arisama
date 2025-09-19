using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Enums;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto>>
{
	public Task<Result<CreatePersonResponseDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		return Person.Create(
			age: new Age(request.Age),
			gender: request.Gender.Match<Gender>(
				onMale: () => new Male(),
				onFemale: () => new Female()
			)
		)
			.Tap(x => dbContext.Persons.Add(x))
			.Tap(x => dbContext.SaveChangesAsync(cancellationToken))
			.Map(x => new CreatePersonResponseDto(Id: x.Id.Value));
	}
}

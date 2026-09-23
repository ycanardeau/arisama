using WebApp.CivilRegistration.Contracts.People.Commands;
using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Contracts.People.Enums;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.People.Commands;

internal class CreatePersonCommandHandler(ApplicationDbContext dbContext)
	: IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto, CivilRegistrationError>>
{
	public Task<Result<CreatePersonResponseDto, CivilRegistrationError>> Handle(
		CreatePersonCommand request,
		CancellationToken cancellationToken
	)
	{
		return Person
			.Create(
				age: new Age(request.Age),
				gender: request.Gender.Match<Gender>(
					onMale: () => new Gender.Male(),
					onFemale: () => new Gender.Female()
				)
			)
			.Tap(x => dbContext.People.Add(x))
			.Tap(x => dbContext.SaveChangesAsync(cancellationToken))
			.Map(x => new CreatePersonResponseDto(Id: x.Id.Value));
	}
}

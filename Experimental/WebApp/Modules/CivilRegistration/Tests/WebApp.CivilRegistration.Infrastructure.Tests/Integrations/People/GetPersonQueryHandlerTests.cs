using WebApp.CivilRegistration.Application.Services.Mappers;
using WebApp.CivilRegistration.Contracts.People.Enums;
using WebApp.CivilRegistration.Contracts.People.Queries;
using WebApp.CivilRegistration.Infrastructure.Integrations.People.Queries;

namespace WebApp.CivilRegistration.Infrastructure.Tests.Integrations.People;

public class GetPersonQueryHandlerTests
{
	private static GetPersonQueryHandler CreateHandler(ApplicationDbContext context) =>
		new(context, new PersonMapper(new MaritalStatusMapper()));

	[Fact]
	public async Task Handle_WhenPersonDoesNotExist_ReturnsPersonNotFound()
	{
		await using var context = TestDb.Create(TestDb.NewName());
		var handler = CreateHandler(context);

		var result = await handler.Handle(
			new GetPersonQuery(Guid.NewGuid()),
			CancellationToken.None
		);

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.PersonNotFound>();
	}

	[Fact]
	public async Task Handle_WhenPersonExists_ReturnsMappedPerson()
	{
		var name = TestDb.NewName();

		Guid id;
		await using (var context = TestDb.Create(name))
		{
			var person = Person.Create(new Age(28), new Gender.Female()).ValueOrThrow();
			context.People.Add(person);
			await context.SaveChangesAsync();
			id = person.Id.Value;
		}

		await using (var context = TestDb.Create(name))
		{
			var handler = CreateHandler(context);

			var result = await handler.Handle(new GetPersonQuery(id), CancellationToken.None);

			result.ErrorOrNull().Should().BeNull();
			var person = result.ValueOrThrow().Person;
			person.Id.Should().Be(id);
			person.Age.Should().Be(28);
			person.Gender.Should().Be(ApiGender.Female);
		}
	}
}

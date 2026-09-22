using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Enums;
using WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Commands;

namespace WebApp.CivilRegistration.Infrastructure.Tests.Integrations.Persons;

public class CreatePersonCommandHandlerTests
{
	[Fact]
	public async Task Handle_PersistsPersonAsSingle_AndReturnsId()
	{
		var name = TestDb.NewName();

		Guid id;
		await using (var context = TestDb.Create(name))
		{
			var handler = new CreatePersonCommandHandler(context);

			var result = await handler.Handle(
				new CreatePersonCommand(Age: 30, Gender: ApiGender.Male),
				CancellationToken.None
			);

			result.ErrorOrNull().Should().BeNull();
			id = result.ValueOrThrow().Id;
			id.Should().NotBeEmpty();
		}

		await using (var context = TestDb.Create(name))
		{
			var person = await context
				.Persons.Include(x => x.MaritalStateMachine)
				.SingleAsync(x => x.Id == new PersonId(id));

			person.Gender.Should().BeOfType<Gender.Male>();
			person.Age.Value.Should().Be(30);
			person.MaritalStateMachine.CurrentState.Should().BeOfType<MaritalStatus.Single>();
		}
	}
}

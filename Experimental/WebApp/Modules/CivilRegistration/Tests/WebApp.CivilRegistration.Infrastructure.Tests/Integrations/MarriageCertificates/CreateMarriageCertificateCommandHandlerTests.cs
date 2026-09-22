using WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;
using WebApp.CivilRegistration.Infrastructure.Integrations.MarriageCertificates.Commands;

namespace WebApp.CivilRegistration.Infrastructure.Tests.Integrations.MarriageCertificates;

public class CreateMarriageCertificateCommandHandlerTests
{
	private static async Task<Guid> SeedPersonAsync(string dbName, Gender gender, int age)
	{
		await using var context = TestDb.Create(dbName);
		var person = Person.Create(new Age(age), gender).ValueOrThrow();
		context.Persons.Add(person);
		await context.SaveChangesAsync();
		return person.Id.Value;
	}

	[Fact]
	public async Task Handle_WhenHusbandDoesNotExist_ReturnsPersonNotFound()
	{
		var name = TestDb.NewName();
		var wifeId = await SeedPersonAsync(name, new Gender.Female(), 28);

		await using var context = TestDb.Create(name);
		var handler = new CreateMarriageCertificateCommandHandler(context);

		var result = await handler.Handle(
			new CreateMarriageCertificateCommand(HusbandId: Guid.NewGuid(), WifeId: wifeId),
			CancellationToken.None
		);

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.PersonNotFound>();
	}

	[Fact]
	public async Task Handle_WithEligibleCouple_PersistsCertificate()
	{
		var name = TestDb.NewName();
		var husbandId = await SeedPersonAsync(name, new Gender.Male(), 30);
		var wifeId = await SeedPersonAsync(name, new Gender.Female(), 28);

		await using var context = TestDb.Create(name);
		var handler = new CreateMarriageCertificateCommandHandler(context);

		var result = await handler.Handle(
			new CreateMarriageCertificateCommand(husbandId, wifeId),
			CancellationToken.None
		);

		result.ErrorOrNull().Should().BeNull();
		(await context.MarriageCertificates.CountAsync()).Should().Be(1);
	}
}

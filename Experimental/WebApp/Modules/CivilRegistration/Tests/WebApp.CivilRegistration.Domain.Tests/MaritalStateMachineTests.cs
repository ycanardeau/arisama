namespace WebApp.CivilRegistration.Domain.Tests;

public class MaritalStateMachineTests
{
	private static Person NewPerson() => TestPeople.Male(30);

	[Fact]
	public void NewlyCreatedPerson_StartsSingle()
	{
		var person = NewPerson();

		person.MaritalStateMachine.CurrentState.Should().BeOfType<MaritalStatus.Single>();
	}

	[Fact]
	public void BecomeWidowed_WhenSingle_FailsWithInvalidMaritalState()
	{
		var person = NewPerson();

		var result = person.BecomeWidowed(new BecomeWidowedCommand());

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.InvalidMaritalState>();
	}

	[Fact]
	public void Divorce_WhenSingle_FailsWithInvalidMaritalState()
	{
		var person = NewPerson();

		var result = person.Divorce(new DivorceCommand(DivorceCertificate: null!));

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.InvalidMaritalState>();
	}
}

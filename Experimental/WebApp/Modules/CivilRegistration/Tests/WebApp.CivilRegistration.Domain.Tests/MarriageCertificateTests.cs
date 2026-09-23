namespace WebApp.CivilRegistration.Domain.Tests;

public class MarriageCertificateTests
{
	[Fact]
	public void Create_WithTheSamePersonTwice_FailsWithSameIndividual()
	{
		var person = TestPeople.Male(30);

		var result = MarriageCertificate.Create(new CreateCommand(Husband: person, Wife: person));

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.SameIndividual>();
	}

	[Fact]
	public void Create_WithSameSexCouple_FailsWithSameSexMarriage()
	{
		var result = MarriageCertificate.Create(
			new CreateCommand(Husband: TestPeople.Male(30), Wife: TestPeople.Male(28))
		);

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.SameSexMarriage>();
	}

	[Fact]
	public void Create_WithFemaleHusband_FailsWithIneligibleHusband()
	{
		var result = MarriageCertificate.Create(
			new CreateCommand(Husband: TestPeople.Female(30), Wife: TestPeople.Male(28))
		);

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.IneligibleHusband>();
	}

	[Fact]
	public void Create_WithUnderageHusband_FailsWithNotMarriageable()
	{
		// The minimum marriageable age for males is 18.
		var result = MarriageCertificate.Create(
			new CreateCommand(Husband: TestPeople.Male(17), Wife: TestPeople.Female(20))
		);

		result.ErrorOrNull().Should().BeOfType<CivilRegistrationError.NotMarriageable>();
	}

	[Fact]
	public void Create_WithEligibleCouple_Succeeds()
	{
		var husband = TestPeople.Male(30);
		var wife = TestPeople.Female(28);

		var result = MarriageCertificate.Create(new CreateCommand(husband, wife));

		result.ErrorOrNull().Should().BeNull();
		var certificate = result.ValueOrThrow();
		certificate.Husband.Should().BeSameAs(husband);
		certificate.Wife.Should().BeSameAs(wife);
	}
}

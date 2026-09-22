namespace WebApp.CivilRegistration.Infrastructure.Tests;

internal static class TestDb
{
	public static string NewName() => Guid.NewGuid().ToString();

	public static ApplicationDbContext Create(string name) =>
		new(
			new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(name).Options,
			new NoopMediator()
		);
}

internal static class ResultTestExtensions
{
	public static T ValueOrThrow<T>(this Result<T, CivilRegistrationError> result) =>
		result.Fold(
			onOk: value => value,
			onError: error =>
				throw new InvalidOperationException(
					$"Expected a successful result but got error '{error.GetType().Name}'."
				)
		);

	public static CivilRegistrationError? ErrorOrNull<T>(
		this Result<T, CivilRegistrationError> result
	) => result.Fold(onOk: _ => (CivilRegistrationError?)null, onError: error => error);
}

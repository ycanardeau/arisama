namespace WebApp.CivilRegistration.Domain.Tests;

internal static class ResultTestExtensions
{
	/// <summary>Unwraps a successful result, or fails the test if it is an error.</summary>
	public static T ValueOrThrow<T>(this Result<T, CivilRegistrationError> result)
	{
		return result.Fold(
			onOk: value => value,
			onError: error =>
				throw new InvalidOperationException(
					$"Expected a successful result but got error '{error.GetType().Name}'."
				)
		);
	}

	/// <summary>Returns the error, or null if the result is successful.</summary>
	public static CivilRegistrationError? ErrorOrNull<T>(
		this Result<T, CivilRegistrationError> result
	)
	{
		return result.Fold(onOk: _ => (CivilRegistrationError?)null, onError: error => error);
	}
}

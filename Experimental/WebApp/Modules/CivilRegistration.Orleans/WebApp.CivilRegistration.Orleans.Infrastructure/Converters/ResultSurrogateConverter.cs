using WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Converters;

file static class CivilRegistrationOrleansErrorMapping
{
	public static CivilRegistrationOrleansErrorKind ToKind(CivilRegistrationOrleansError error)
	{
		return error.Match(
			AlreadyInitialized: _ => CivilRegistrationOrleansErrorKind.AlreadyInitialized,
			InvalidMaritalState: _ => CivilRegistrationOrleansErrorKind.InvalidMaritalState
		);
	}

	public static CivilRegistrationOrleansError FromKind(CivilRegistrationOrleansErrorKind kind)
	{
		return kind switch
		{
			CivilRegistrationOrleansErrorKind.AlreadyInitialized =>
				new CivilRegistrationOrleansError.AlreadyInitialized(),
			_ => new CivilRegistrationOrleansError.InvalidMaritalState(),
		};
	}
}

[RegisterConverter]
internal sealed class ResultSurrogateConverter
	: IConverter<Result<Unit, CivilRegistrationOrleansError>, ResultSurrogate>
{
	public Result<Unit, CivilRegistrationOrleansError> ConvertFromSurrogate(
		in ResultSurrogate surrogate
	)
	{
		if (surrogate.IsOk)
		{
			return Ok();
		}

		return CivilRegistrationOrleansErrorMapping.FromKind(surrogate.Error);
	}

	public ResultSurrogate ConvertToSurrogate(in Result<Unit, CivilRegistrationOrleansError> value)
	{
		return value.Fold(
			onOk: _ => new ResultSurrogate { IsOk = true },
			onError: error => new ResultSurrogate
			{
				IsOk = false,
				Error = CivilRegistrationOrleansErrorMapping.ToKind(error),
			}
		);
	}
}

using Nut.Results;
using WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Converters;

[RegisterConverter]
internal sealed class ResultSurrogateConverter : IConverter<Result, ResultSurrogate>
{
	public Result ConvertFromSurrogate(in ResultSurrogate surrogate)
	{
		return surrogate.IsOk
			? Result.Ok()
			: Result.Error(surrogate.Exception);
	}

	public ResultSurrogate ConvertToSurrogate(in Result value)
	{
		return new ResultSurrogate
		{
			IsOk = value.IsOk,
			Exception = value.IsOk
				? null!
				: value.GetError(),
		};
	}
}

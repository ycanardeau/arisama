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

[RegisterConverter]
internal sealed class ResultSurrogateConverter<T> : IConverter<Result<T>, ResultSurrogate<T>>
{
	public Result<T> ConvertFromSurrogate(in ResultSurrogate<T> surrogate)
	{
		return surrogate.IsOk
			? Result.Ok(surrogate.Value)
			: Result.Error<T>(surrogate.Exception);
	}

	public ResultSurrogate<T> ConvertToSurrogate(in Result<T> value)
	{
		return new ResultSurrogate<T>
		{
			IsOk = value.IsOk,
			Exception = value.IsOk
				? null!
				: value.GetError(),
			Value = value.IsOk
				? value.Get()
				: default!,
		};
	}
}

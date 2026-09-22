using WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Converters;

file static class WebAppErrorMapping
{
	public static WebAppErrorKind ToKind(WebAppError error)
	{
		return error.Match(
			onBadRequest: _ => WebAppErrorKind.BadRequest,
			onUnauthorized: _ => WebAppErrorKind.Unauthorized,
			onForbidden: _ => WebAppErrorKind.Forbidden,
			onNotFound: _ => WebAppErrorKind.NotFound,
			onUnprocessableEntity: _ => WebAppErrorKind.UnprocessableEntity,
			onUnexpected: _ => WebAppErrorKind.Unexpected
		);
	}

	public static WebAppError FromKind(WebAppErrorKind kind)
	{
		return kind switch
		{
			WebAppErrorKind.BadRequest => new WebAppError.BadRequest(),
			WebAppErrorKind.Unauthorized => new WebAppError.Unauthorized(),
			WebAppErrorKind.Forbidden => new WebAppError.Forbidden(),
			WebAppErrorKind.NotFound => new WebAppError.NotFound(),
			WebAppErrorKind.UnprocessableEntity => new WebAppError.UnprocessableEntity(),
			_ => new WebAppError.Unexpected(),
		};
	}
}

[RegisterConverter]
internal sealed class ResultSurrogateConverter
	: IConverter<Result<Unit, WebAppError>, ResultSurrogate>
{
	public Result<Unit, WebAppError> ConvertFromSurrogate(in ResultSurrogate surrogate)
	{
		if (surrogate.IsOk)
		{
			return Ok();
		}

		return WebAppErrorMapping.FromKind(surrogate.Error);
	}

	public ResultSurrogate ConvertToSurrogate(in Result<Unit, WebAppError> value)
	{
		return value.Fold(
			onOk: _ => new ResultSurrogate { IsOk = true },
			onError: error => new ResultSurrogate
			{
				IsOk = false,
				Error = WebAppErrorMapping.ToKind(error),
			}
		);
	}
}

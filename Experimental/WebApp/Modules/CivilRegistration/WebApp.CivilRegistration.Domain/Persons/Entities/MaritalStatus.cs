using System.Diagnostics;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus
{
	public MaritalStatusId Id { get; set; }
	public MaritalStateMachineId StateMachineId { get; set; }
	public MaritalStateMachine StateMachine { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }
}

internal abstract class MaritalStatus<TPayload> : MaritalStatus
	where TPayload : MaritalStatusPayload
{
	public required TPayload Payload { get; init; }
}

internal sealed class Single : MaritalStatus<SinglePayload>
	, ICanMarry;

internal sealed class Married : MaritalStatus<MarriedPayload>
	, ICanDivorce
	, ICanBecomeWidowed
{
	PersonId ICanDivorce.DivorcedFromId => Payload.MarriedWithId;
}

internal sealed class Divorced : MaritalStatus<DivorcedPayload>
	, ICanMarry;

internal sealed class Widowed : MaritalStatus<WidowedPayload>
	, ICanMarry;

internal static class MaritalStatusExtensions
{
	public static U Match<U>(
		this MaritalStatus state,
		Func<Single, U> onSingle,
		Func<Married, U> onMarried,
		Func<Divorced, U> onDivorced,
		Func<Widowed, U> onWidowed
	)
	{
		return state switch
		{
			Single x => onSingle(x),
			Married x => onMarried(x),
			Divorced x => onDivorced(x),
			Widowed x => onWidowed(x),
			_ => throw new UnreachableException(),
		};
	}
}

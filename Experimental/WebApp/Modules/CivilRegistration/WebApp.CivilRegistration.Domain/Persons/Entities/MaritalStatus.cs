using System.Diagnostics;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus : Entity<MaritalStatusId>
{
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
	, ICanDecease
	, ICanMarry
{
	PersonId? ICanDecease.WidowedId => null;
}

internal sealed class Married : MaritalStatus<MarriedPayload>
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed
{
	PersonId? ICanDecease.WidowedId => Payload.MarriedWithId;

	PersonId ICanDivorce.DivorcedFromId => Payload.MarriedWithId;

	PersonId ICanBecomeWidowed.WidowedFromId => Payload.MarriedWithId;
}

internal sealed class Divorced : MaritalStatus<DivorcedPayload>
	, ICanDecease
	, ICanMarry
{
	PersonId? ICanDecease.WidowedId => null;
}

internal sealed class Widowed : MaritalStatus<WidowedPayload>
	, ICanDecease
	, ICanMarry
{
	PersonId? ICanDecease.WidowedId => null;
}

internal sealed class Deceased : MaritalStatus<DeceasedPayload>;

internal static class MaritalStatusExtensions
{
	public static U Match<U>(
		this MaritalStatus state,
		Func<Single, U> onSingle,
		Func<Married, U> onMarried,
		Func<Divorced, U> onDivorced,
		Func<Widowed, U> onWidowed,
		Func<Deceased, U> onDeceased
	)
	{
		return state switch
		{
			Single x => onSingle(x),
			Married x => onMarried(x),
			Divorced x => onDivorced(x),
			Widowed x => onWidowed(x),
			Deceased x => onDeceased(x),
			_ => throw new UnreachableException(),
		};
	}
}

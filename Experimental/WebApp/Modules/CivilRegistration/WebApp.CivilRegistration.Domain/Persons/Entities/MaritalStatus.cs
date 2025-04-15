using System.Diagnostics;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus : Entity<MaritalStatusId>, IMaritalStatus
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

internal sealed class SingleState : MaritalStatus<SingleStatePayload>
	, ICanDecease
	, ICanMarry
{
}

internal sealed class MarriedState : MaritalStatus<MarriedStatePayload>
	, IHasMarriageInformation
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed
{
	MarriageInformation IHasMarriageInformation.MarriageInformation => Payload.MarriageInformation;
}

internal sealed class DivorcedState : MaritalStatus<DivorcedStatePayload>
	, IHasMarriageInformation
	, IHasDivorceInformation
	, ICanDecease
	, ICanMarry
{
	MarriageInformation IHasMarriageInformation.MarriageInformation => Payload.MarriageInformation;

	DivorceInformation IHasDivorceInformation.DivorceInformation => Payload.DivorceInformation;
}

internal sealed class WidowedState : MaritalStatus<WidowedStatePayload>
	, IHasMarriageInformation
	, IHasWidowhoodInformation
	, ICanDecease
	, ICanMarry
{
	MarriageInformation IHasMarriageInformation.MarriageInformation => Payload.MarriageInformation;

	WidowhoodInformation IHasWidowhoodInformation.WidowhoodInformation => Payload.WidowhoodInformation;
}

internal sealed class DeceasedState : MaritalStatus<DeceasedStatePayload>
	, IHasDeathInformation
{
	DeathInformation IHasDeathInformation.DeathInformation => Payload.DeathInformation;
}

internal static class MaritalStatusExtensions
{
	public static U Match<U>(
		this MaritalStatus state,
		Func<SingleState, U> onSingleState,
		Func<MarriedState, U> onMarriedState,
		Func<DivorcedState, U> onDivorcedState,
		Func<WidowedState, U> onWidowedState,
		Func<DeceasedState, U> onDeceasedState
	)
	{
		return state switch
		{
			SingleState x => onSingleState(x),
			MarriedState x => onMarriedState(x),
			DivorcedState x => onDivorcedState(x),
			WidowedState x => onWidowedState(x),
			DeceasedState x => onDeceasedState(x),
			_ => throw new UnreachableException(),
		};
	}
}

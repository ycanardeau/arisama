using System.Diagnostics;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.IMaritalTransition;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatusPayload;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus
{
	public abstract class WithPayload<TPayload> : MaritalStatus
		where TPayload : MaritalStatusPayload
	{
		public required TPayload Payload { get; init; }
	}

	public sealed class Single : WithPayload<SinglePayload>
		, ICanMarry;

	public sealed class Married : WithPayload<MarriedPayload>
		, ICanDivorce
		, ICanBecomeWidowed;

	public sealed class Divorced : WithPayload<DivorcedPayload>
		, ICanMarry;

	public sealed class Widowed : WithPayload<WidowedPayload>
		, ICanMarry;

	public U Match<U>(
		Func<Single, U> onSingle,
		Func<Married, U> onMarried,
		Func<Divorced, U> onDivorced,
		Func<Widowed, U> onWidowed
	)
	{
		return this switch
		{
			Single x => onSingle(x),
			Married x => onMarried(x),
			Divorced x => onDivorced(x),
			Widowed x => onWidowed(x),
			_ => throw new UnreachableException(),
		};
	}

	public MaritalStatusId Id { get; set; }
	public MaritalStateMachineId StateMachineId { get; set; }
	public MaritalStateMachine StateMachine { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }

	private MaritalStatus() { }
}

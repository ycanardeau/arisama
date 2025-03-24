using System.Diagnostics;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.IMaritalTransition;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus
{
	public MaritalStatusId Id { get; set; }
	public MaritalStateMachineId StateMachineId { get; set; }
	public MaritalStateMachine StateMachine { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }

	private MaritalStatus() { }

	public sealed class Single : MaritalStatus
		, ICanMarry;

	public sealed class Married : MaritalStatus
		, ICanDivorce
		, ICanBecomeWidowed;

	public sealed class Divorced : MaritalStatus
		, ICanMarry;

	public sealed class Widowed : MaritalStatus
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
}

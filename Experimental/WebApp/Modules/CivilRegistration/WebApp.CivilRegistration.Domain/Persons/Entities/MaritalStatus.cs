using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.IMaritalTransition;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract class MaritalStatus
{
	public MaritalStatusId Id { get; set; }
	public MaritalStateMachineId StateMachineId { get; set; }
	public MaritalStateMachine StateMachine { get; set; } = default!;
	public required MaritalStatusVersion Version { get; set; }

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
}

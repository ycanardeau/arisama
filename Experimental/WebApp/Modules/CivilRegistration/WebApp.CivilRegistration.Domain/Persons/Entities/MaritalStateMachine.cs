using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.IMaritalTransition;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class MaritalStateMachine
{
	public MaritalStateMachineId Id { get; set; }
	public PersonId PersonId { get; set; }
	public Person Person { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }

	public ICollection<MaritalStatus> States { get; set; } = [];

	private MaritalStatusVersion IncrementVersion()
	{
		Version++;
		return Version;
	}

	public void AddState(Func<MaritalStatusVersion, MaritalStatus> stateFactory)
	{
		States.Add(stateFactory(IncrementVersion()));
	}

	public MaritalStatus CurrentState => States.MaxBy(x => x.Version) ?? throw new InvalidOperationException("Sequence contains no elements");

	private Result<MaritalStateMachine, InvalidOperationException> ExecuteIf<TTransition, TCommand>(TCommand command)
		where TCommand : MaritalCommand
		where TTransition : IMaritalTransition<TCommand>
	{
		return CurrentState is not TTransition transition
			? Result.Error(new InvalidOperationException($"{nameof(CurrentState)} is not {typeof(TTransition).Name}"))
			: transition.Execute(this, command);
	}

	public Result<MaritalStateMachine, InvalidOperationException> Marry(MarryCommand command)
	{
		return ExecuteIf<ICanMarry, MarryCommand>(command);
	}

	public Result<MaritalStateMachine, InvalidOperationException> Divorce(DivorceCommand command)
	{
		return ExecuteIf<ICanDivorce, DivorceCommand>(command);
	}

	public Result<MaritalStateMachine, InvalidOperationException> BecomeWidowed(BecomeWidowedCommand command)
	{
		return ExecuteIf<ICanBecomeWidowed, BecomeWidowedCommand>(command);
	}
}

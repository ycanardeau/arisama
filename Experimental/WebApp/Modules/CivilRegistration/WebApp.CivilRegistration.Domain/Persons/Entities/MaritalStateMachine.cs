using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.IMaritalTransition;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class MaritalStateMachine
{
	public MaritalStateMachineId Id { get; set; }
	public PersonId PersonId { get; set; }
	public Person Person { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }

	public ICollection<MaritalStatus> States { get; set; } = [];

	private MaritalStateMachine() { }

	private MaritalStatusVersion IncrementVersion()
	{
		Version++;
		return Version;
	}

	private TNextState AddState<TNextState>(TNextState nextState)
		where TNextState : MaritalStatus
	{
		nextState.Version = IncrementVersion();

		States.Add(nextState);

		return nextState;
	}

	public static Result<MaritalStateMachine, InvalidOperationException> Create()
	{
		var stateMachine = new MaritalStateMachine();

		stateMachine.AddState(new MaritalStatus.Single());

		return Result.Ok(stateMachine);
	}

	public MaritalStatus CurrentState => States.MaxBy(x => x.Version) ?? throw new InvalidOperationException("Sequence contains no elements");

	private Result<TNextState, InvalidOperationException> ExecuteIf<TTransition, TCommand, TNextState>(TCommand command)
		where TCommand : MaritalCommand
		where TNextState : MaritalStatus
		where TTransition : IMaritalTransition<TCommand, TNextState>
	{
		return CurrentState is not TTransition transition
			? Result.Error(new InvalidOperationException($"{nameof(CurrentState)} is not {typeof(TTransition).Name}"))
			: transition.Execute(this, command)
				.Map(AddState);
	}

	public Result<Married, InvalidOperationException> Marry(MarryCommand command)
	{
		return ExecuteIf<ICanMarry, MarryCommand, Married>(command);
	}

	public Result<Divorced, InvalidOperationException> Divorce(DivorceCommand command)
	{
		return ExecuteIf<ICanDivorce, DivorceCommand, Divorced>(command);
	}

	public Result<Widowed, InvalidOperationException> BecomeWidowed(BecomeWidowedCommand command)
	{
		return ExecuteIf<ICanBecomeWidowed, BecomeWidowedCommand, Widowed>(command);
	}
}

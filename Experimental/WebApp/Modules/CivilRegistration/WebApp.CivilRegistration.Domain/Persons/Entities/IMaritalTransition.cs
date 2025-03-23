using DiscriminatedOnions;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition
{
	public interface ICanMarry : IMaritalTransition<MarryCommand>
	{
		Result<MaritalStateMachine, InvalidOperationException> IMaritalTransition<MarryCommand>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
		{
			stateMachine.AddState(x => new Married
			{
				Version = x,
			});

			return Result.Ok(stateMachine);
		}
	}

	public interface ICanDivorce : IMaritalTransition<DivorceCommand>
	{
		Result<MaritalStateMachine, InvalidOperationException> IMaritalTransition<DivorceCommand>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
		{
			stateMachine.AddState(x => new Divorced
			{
				Version = x,
			});

			return Result.Ok(stateMachine);
		}
	}

	public interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand>
	{
		Result<MaritalStateMachine, InvalidOperationException> IMaritalTransition<BecomeWidowedCommand>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
		{
			stateMachine.AddState(x => new Widowed
			{
				Version = x,
			});

			return Result.Ok(stateMachine);
		}
	}
}

internal interface IMaritalTransition<TCommand> : IMaritalTransition
	where TCommand : MaritalCommand
{
	Result<MaritalStateMachine, InvalidOperationException> Execute(MaritalStateMachine stateMachine, TCommand command);
}

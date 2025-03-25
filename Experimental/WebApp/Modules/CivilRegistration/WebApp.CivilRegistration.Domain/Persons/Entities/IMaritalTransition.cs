using DiscriminatedOnions;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition
{
	public interface ICanMarry : IMaritalTransition<MarryCommand, Married>
	{
		Result<Married, InvalidOperationException> IMaritalTransition<MarryCommand, Married>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
		{
			return Result.Ok(new Married
			{
				Payload = new(),
			});
		}
	}

	public interface ICanDivorce : IMaritalTransition<DivorceCommand, Divorced>
	{
		Result<Divorced, InvalidOperationException> IMaritalTransition<DivorceCommand, Divorced>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
		{
			return Result.Ok(new Divorced
			{
				Payload = new(),
			});
		}
	}

	public interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand, Widowed>
	{
		Result<Widowed, InvalidOperationException> IMaritalTransition<BecomeWidowedCommand, Widowed>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
		{
			return Result.Ok(new Widowed
			{
				Payload = new(),
			});
		}
	}
}

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState, InvalidOperationException> Execute(MaritalStateMachine stateMachine, TCommand command);
}

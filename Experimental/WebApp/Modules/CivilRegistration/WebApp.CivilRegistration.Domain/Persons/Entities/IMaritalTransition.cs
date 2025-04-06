using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition;

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState, InvalidOperationException> Execute(MaritalStateMachine stateMachine, TCommand command);
}

internal interface ICanMarry : IMaritalTransition<MarryCommand, Married>
{
	Result<Married, InvalidOperationException> IMaritalTransition<MarryCommand, Married>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
	{
		return Result.Ok(new Married
		{
			Payload = new(MarriedWithId: command.MarryWith.Id),
		});
	}
}

internal interface ICanDivorce : IMaritalTransition<DivorceCommand, Divorced>
{
	PersonId DivorcedFromId { get; }

	Result<Divorced, InvalidOperationException> IMaritalTransition<DivorceCommand, Divorced>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return Result.Ok(new Divorced
		{
			Payload = new(DivorcedFromId),
		});
	}
}

internal interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand, Widowed>
{
	PersonId WidowedFromId { get; }

	Result<Widowed, InvalidOperationException> IMaritalTransition<BecomeWidowedCommand, Widowed>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return Result.Ok(new Widowed
		{
			Payload = new(WidowedFromId),
		});
	}
}

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, Deceased>
{
	PersonId? WidowedId { get; }

	Result<Deceased, InvalidOperationException> IMaritalTransition<DeceaseCommand, Deceased>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return Result.Ok(new Deceased
		{
			Payload = new(WidowedId),
		});
	}
}

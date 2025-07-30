using Nut.Results;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition;

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState> Execute(MaritalStateMachine stateMachine, TCommand command);
}

internal interface ICanMarry : IMaritalTransition<MarryCommand, MarriedState>
{
	Result<MarriedState> IMaritalTransition<MarryCommand, MarriedState>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
	{
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error<MarriedState>(new InvalidOperationException("Not of marriageable age"))
			: new MarriedState(MarriageInformation: new(
				MarriageCertificateId: command.MarriageCertificate.Id,
				MarriedAtAge: stateMachine.Person.Age,
				MarriedWithId: command.MarryWith.Id
			));
	}
}

internal interface ICanDivorce : IMaritalTransition<DivorceCommand, DivorcedState>
	, IHasMarriageInformation
{
	Result<DivorcedState> IMaritalTransition<DivorceCommand, DivorcedState>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return new DivorcedState(
			MarriageInformation,
			DivorceInformation: new(
				DivorceCertificateId: command.DivorceCertificate.Id,
				DivorcedAtAge: stateMachine.Person.Age,
				DivorcedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}

internal interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand, WidowedState>
	, IHasMarriageInformation
{
	Result<WidowedState> IMaritalTransition<BecomeWidowedCommand, WidowedState>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return new WidowedState(
			MarriageInformation,
			WidowhoodInformation: new(
				WidowedAtAge: stateMachine.Person.Age,
				WidowedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, DeceasedState>
{
	Result<DeceasedState> IMaritalTransition<DeceaseCommand, DeceasedState>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return new DeceasedState(DeathInformation: new(
			DeathCertificateId: command.DeathCertificate.Id,
			DeceasedAtAge: stateMachine.Person.Age
		));
	}
}

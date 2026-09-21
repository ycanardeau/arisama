using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition;

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState> Execute(MaritalStateMachine stateMachine, TCommand command);
}

internal interface ICanMarry : IMaritalTransition<MarryCommand, MaritalStatus.Married>
{
	Result<MaritalStatus.Married> IMaritalTransition<MarryCommand, MaritalStatus.Married>.Execute(
		MaritalStateMachine stateMachine,
		MarryCommand command
	)
	{
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error<MaritalStatus.Married>(
				new InvalidOperationException("Not of marriageable age")
			)
			: new MaritalStatus.Married(
				MarriageInformation: new(
					MarriageCertificateId: command.MarriageCertificate.Id,
					MarriedAtAge: stateMachine.Person.Age,
					MarriedWithId: command.MarryWith.Id
				)
			);
	}
}

internal interface ICanDivorce
	: IMaritalTransition<DivorceCommand, MaritalStatus.Divorced>,
		IHasMarriageInformation
{
	Result<MaritalStatus.Divorced> IMaritalTransition<
		DivorceCommand,
		MaritalStatus.Divorced
	>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return new MaritalStatus.Divorced(
			MarriageInformation,
			DivorceInformation: new(
				DivorceCertificateId: command.DivorceCertificate.Id,
				DivorcedAtAge: stateMachine.Person.Age,
				DivorcedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}

internal interface ICanBecomeWidowed
	: IMaritalTransition<BecomeWidowedCommand, MaritalStatus.Widowed>,
		IHasMarriageInformation
{
	Result<MaritalStatus.Widowed> IMaritalTransition<
		BecomeWidowedCommand,
		MaritalStatus.Widowed
	>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return new MaritalStatus.Widowed(
			MarriageInformation,
			WidowhoodInformation: new(
				WidowedAtAge: stateMachine.Person.Age,
				WidowedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, MaritalStatus.Deceased>
{
	Result<MaritalStatus.Deceased> IMaritalTransition<
		DeceaseCommand,
		MaritalStatus.Deceased
	>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return new MaritalStatus.Deceased(
			DeathInformation: new(
				DeathCertificateId: command.DeathCertificate.Id,
				DeceasedAtAge: stateMachine.Person.Age
			)
		);
	}
}

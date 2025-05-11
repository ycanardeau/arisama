using DiscriminatedOnions;

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
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error(new InvalidOperationException("Not of marriageable age"))
			: Result.Ok(new Married
			{
				Payload = new(MarriageInformation: new(
					MarriageCertificateGuid: command.MarriageCertificate.Guid,
					MarriedAtAge: stateMachine.Person.Age,
					MarriedWithId: command.MarryWith.Id
				)),
			});
	}
}

internal interface ICanDivorce : IMaritalTransition<DivorceCommand, Divorced>
	, IHasMarriageInformation
{
	Result<Divorced, InvalidOperationException> IMaritalTransition<DivorceCommand, Divorced>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return Result.Ok(new Divorced
		{
			Payload = new(
				MarriageInformation,
				DivorceInformation: new(
					DivorceCertificateGuid: command.DivorceCertificate.Guid,
					DivorcedAtAge: stateMachine.Person.Age,
					DivorcedFromId: MarriageInformation.MarriedWithId
				)
			),
		});
	}
}

internal interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand, Widowed>
	, IHasMarriageInformation
{
	Result<Widowed, InvalidOperationException> IMaritalTransition<BecomeWidowedCommand, Widowed>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return Result.Ok(new Widowed
		{
			Payload = new(
				MarriageInformation,
				WidowhoodInformation: new(
					WidowedAtAge: stateMachine.Person.Age,
					WidowedFromId: MarriageInformation.MarriedWithId
				)
			),
		});
	}
}

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, Deceased>
{
	Result<Deceased, InvalidOperationException> IMaritalTransition<DeceaseCommand, Deceased>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return Result.Ok(new Deceased
		{
			Payload = new(new(
				DeathCertificateGuid: command.DeathCertificate.Guid,
				DeceasedAtAge: stateMachine.Person.Age
			)),
		});
	}
}

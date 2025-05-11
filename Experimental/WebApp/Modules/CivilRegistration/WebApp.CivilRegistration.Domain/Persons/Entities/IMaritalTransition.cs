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
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error(new InvalidOperationException("Not of marriageable age"))
			: Result.Ok(new Married
			{
				Id = MaritalStatusId.CreateVersion7(),
				Payload = new(MarriageInformation: new(
					MarriageCertificateId: command.MarriageCertificate.Id,
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
			Id = MaritalStatusId.CreateVersion7(),
			Payload = new(
				MarriageInformation,
				DivorceInformation: new(
					DivorceCertificateId: command.DivorceCertificate.Id,
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
			Id = MaritalStatusId.CreateVersion7(),
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
			Id = MaritalStatusId.CreateVersion7(),
			Payload = new(new(
				DeathCertificateId: command.DeathCertificate.Id,
				DeceasedAtAge: stateMachine.Person.Age
			)),
		});
	}
}

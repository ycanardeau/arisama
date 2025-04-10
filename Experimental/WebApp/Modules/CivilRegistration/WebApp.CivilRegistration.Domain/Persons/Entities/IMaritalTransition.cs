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
				Payload = new(
					MarriageCertificateId: command.MarriageCertificate.Id,
					MarriedAtAge: stateMachine.Person.Age,
					MarriedWithId: command.MarryWith.Id
				),
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
			Payload = new(
				DivorceCertificateId: command.DivorceCertificate.Id,
				DivorcedAtAge: stateMachine.Person.Age,
				DivorcedFromId
			),
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
			Payload = new(
				WidowedAtAge: stateMachine.Person.Age,
				WidowedFromId
			),
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
			Payload = new(
				DeathCertificateId: command.DeathCertificate.Id,
				DeceasedAtAge: stateMachine.Person.Age,
				WidowedId
			),
		});
	}
}

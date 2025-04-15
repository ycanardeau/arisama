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
				Payload = new()
				{
					MarriageInformation = new()
					{
						MarriageCertificate = command.MarriageCertificate,
						MarriedAtAge = stateMachine.Person.Age,
						MarriedWith = command.MarryWith,
					},
				},
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
			Payload = new()
			{
				MarriageInformation = MarriageInformation,
				DivorceInformation = new()
				{
					DivorceCertificate = command.DivorceCertificate,
					DivorcedAtAge = stateMachine.Person.Age,
					DivorcedFrom = MarriageInformation.MarriedWith,
				},
			},
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
			Payload = new()
			{
				MarriageInformation = MarriageInformation,
				WidowhoodInformation = new()
				{
					WidowedAtAge = stateMachine.Person.Age,
					WidowedFrom = MarriageInformation.MarriedWith,
				},
			},
		});
	}
}

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, Deceased>
{
	Result<Deceased, InvalidOperationException> IMaritalTransition<DeceaseCommand, Deceased>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return Result.Ok(new Deceased
		{
			Payload = new()
			{
				DeathInformation = new()
				{
					DeathCertificate = command.DeathCertificate,
					DeceasedAtAge = stateMachine.Person.Age,
				},
			},
		});
	}
}

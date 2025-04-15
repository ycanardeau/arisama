using DiscriminatedOnions;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalTransition;

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState, InvalidOperationException> Execute(MaritalStateMachine stateMachine, TCommand command);
}

internal interface ICanMarry : IMaritalTransition<MarryCommand, MarriedState>
{
	Result<MarriedState, InvalidOperationException> IMaritalTransition<MarryCommand, MarriedState>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
	{
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error(new InvalidOperationException("Not of marriageable age"))
			: Result.Ok(new MarriedState
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

internal interface ICanDivorce : IMaritalTransition<DivorceCommand, DivorcedState>
	, IHasMarriageInformation
{
	Result<DivorcedState, InvalidOperationException> IMaritalTransition<DivorceCommand, DivorcedState>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return Result.Ok(new DivorcedState
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

internal interface ICanBecomeWidowed : IMaritalTransition<BecomeWidowedCommand, WidowedState>
	, IHasMarriageInformation
{
	Result<WidowedState, InvalidOperationException> IMaritalTransition<BecomeWidowedCommand, WidowedState>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return Result.Ok(new WidowedState
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

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, DeceasedState>
{
	Result<DeceasedState, InvalidOperationException> IMaritalTransition<DeceaseCommand, DeceasedState>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return Result.Ok(new DeceasedState
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

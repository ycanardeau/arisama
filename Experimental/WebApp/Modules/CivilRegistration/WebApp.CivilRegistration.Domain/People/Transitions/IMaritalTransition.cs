using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Transitions;

internal interface IMaritalTransition;

internal interface IMaritalTransition<TCommand, TNextState> : IMaritalTransition
	where TCommand : MaritalCommand
	where TNextState : MaritalStatus
{
	Result<TNextState, CivilRegistrationError> Execute(
		MaritalStateMachine stateMachine,
		TCommand command
	);
}

using WebApp.CivilRegistration.Domain.Common.Events;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Events;

internal sealed record MaritalStatusChangedDomainEvent(
	MaritalStateMachine StateMachine,
	MaritalStatus State
) : IDomainEvent;

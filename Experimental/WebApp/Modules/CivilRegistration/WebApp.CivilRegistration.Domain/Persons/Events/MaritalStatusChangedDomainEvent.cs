using WebApp.CivilRegistration.Domain.Common.Events;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Domain.Persons.Events;

internal sealed record MaritalStatusChangedDomainEvent(MaritalStateMachine StateMachine, MaritalStatus State) : IDomainEvent;

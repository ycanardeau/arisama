using WebApp.CivilRegistration.Domain.Common.Events;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Events;

internal sealed record MaritalStatusChangedDomainEvent(MaritalStateMachine StateMachine, MaritalStatus State) : IDomainEvent;

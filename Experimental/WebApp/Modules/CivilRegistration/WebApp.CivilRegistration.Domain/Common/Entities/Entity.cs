using WebApp.CivilRegistration.Domain.Common.Events;

namespace WebApp.CivilRegistration.Domain.Common.Entities;

internal abstract class Entity
{
	private readonly List<IDomainEvent> _domainEvents = [];
	public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

	public void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}
}

internal abstract class Entity<TId> : Entity
	where TId : struct
{
	public required TId Id { get; set; }
}

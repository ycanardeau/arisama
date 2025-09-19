using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure;

internal static class MediatorExtensions
{
	public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
	{
		var domainEntities = ctx.ChangeTracker
			.Entries<Entity>()
			.Where(x => x.Entity.DomainEvents.Count != 0);

		var domainEvents = domainEntities
			.SelectMany(x => x.Entity.DomainEvents)
			.ToList();

		domainEntities.ToList()
			.ForEach(entity => entity.Entity.ClearDomainEvents());

		foreach (var domainEvent in domainEvents)
		{
			await mediator.Publish(domainEvent);
		}
	}
}

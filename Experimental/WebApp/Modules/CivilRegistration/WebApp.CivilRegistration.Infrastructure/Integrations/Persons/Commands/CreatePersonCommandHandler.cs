using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Infrastructure.Persistence;

namespace WebApp.CivilRegistration.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto, InvalidOperationException>>
{
	public Task<Result<CreatePersonResponseDto, InvalidOperationException>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		return Person.Create()
			.MapAsync(async x =>
			{
				dbContext.Persons.Add(x);

				await dbContext.SaveChangesAsync(cancellationToken);

				return new CreatePersonResponseDto(Id: x.Id.Value);
			});
	}
}

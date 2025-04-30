using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler() : IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto, InvalidOperationException>>
{
	public Task<Result<CreatePersonResponseDto, InvalidOperationException>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

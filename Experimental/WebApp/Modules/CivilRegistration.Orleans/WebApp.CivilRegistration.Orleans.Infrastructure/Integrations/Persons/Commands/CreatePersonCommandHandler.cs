using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler() : IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto>>
{
	public Task<Result<CreatePersonResponseDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

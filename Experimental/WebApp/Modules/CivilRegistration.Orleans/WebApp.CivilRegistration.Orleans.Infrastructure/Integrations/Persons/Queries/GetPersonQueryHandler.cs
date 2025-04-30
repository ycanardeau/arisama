using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Queries;

internal class GetPersonQueryHandler(
) : IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto>>
{
	public Task<Result<GetPersonResponseDto>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

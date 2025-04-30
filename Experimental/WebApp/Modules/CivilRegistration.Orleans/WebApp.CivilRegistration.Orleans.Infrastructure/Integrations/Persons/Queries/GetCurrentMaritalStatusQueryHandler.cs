using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Queries;

internal class GetCurrentMaritalStatusEndpoint(
) : IRequestHandler<GetCurrentMaritalStatusQuery, Result<GetCurrentMaritalStatusResponseDto>>
{
	public Task<Result<GetCurrentMaritalStatusResponseDto>> Handle(GetCurrentMaritalStatusQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

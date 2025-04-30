using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Queries;

internal class GetCurrentMaritalStatusEndpoint(
) : IRequestHandler<GetCurrentMaritalStatusQuery, Result<GetCurrentMaritalStatusResponseDto, InvalidOperationException>>
{
	public Task<Result<GetCurrentMaritalStatusResponseDto, InvalidOperationException>> Handle(GetCurrentMaritalStatusQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

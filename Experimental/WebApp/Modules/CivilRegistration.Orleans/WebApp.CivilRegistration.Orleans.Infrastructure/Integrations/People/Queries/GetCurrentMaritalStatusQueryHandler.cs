using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.People.Queries;

internal class GetCurrentMaritalStatusEndpoint()
	: IRequestHandler<
		GetCurrentMaritalStatusQuery,
		Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationOrleansError>
	>
{
	public Task<Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationOrleansError>> Handle(
		GetCurrentMaritalStatusQuery request,
		CancellationToken cancellationToken
	)
	{
		throw new NotImplementedException();
	}
}

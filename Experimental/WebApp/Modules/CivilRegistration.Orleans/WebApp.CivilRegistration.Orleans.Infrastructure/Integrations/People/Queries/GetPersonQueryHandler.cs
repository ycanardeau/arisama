using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.People.Queries;

internal class GetPersonQueryHandler()
	: IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto, CivilRegistrationOrleansError>>
{
	public Task<Result<GetPersonResponseDto, CivilRegistrationOrleansError>> Handle(
		GetPersonQuery request,
		CancellationToken cancellationToken
	)
	{
		throw new NotImplementedException();
	}
}

using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Queries;

internal class ListPersonsQueryHandler()
	: IRequestHandler<
		ListPersonsQuery,
		Result<ListPersonsResponseDto, CivilRegistrationOrleansError>
	>
{
	public Task<Result<ListPersonsResponseDto, CivilRegistrationOrleansError>> Handle(
		ListPersonsQuery request,
		CancellationToken cancellationToken
	)
	{
		throw new NotImplementedException();
	}
}

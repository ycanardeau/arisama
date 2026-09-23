using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.People.Queries;

internal class ListPeopleQueryHandler()
	: IRequestHandler<ListPeopleQuery, Result<ListPeopleResponseDto, CivilRegistrationOrleansError>>
{
	public Task<Result<ListPeopleResponseDto, CivilRegistrationOrleansError>> Handle(
		ListPeopleQuery request,
		CancellationToken cancellationToken
	)
	{
		throw new NotImplementedException();
	}
}

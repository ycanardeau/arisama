using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Queries;

internal class GetPersonQueryHandler(
) : IRequestHandler<GetPersonQuery, Result<GetPersonResponseDto, InvalidOperationException>>
{
	public Task<Result<GetPersonResponseDto, InvalidOperationException>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

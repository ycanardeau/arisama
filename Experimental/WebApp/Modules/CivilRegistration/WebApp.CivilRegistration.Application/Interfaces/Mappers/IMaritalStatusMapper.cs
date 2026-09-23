using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Application.Interfaces.Mappers;

internal interface IMaritalStatusMapper
{
	MaritalStatusDto Map(MaritalStatus value);
}

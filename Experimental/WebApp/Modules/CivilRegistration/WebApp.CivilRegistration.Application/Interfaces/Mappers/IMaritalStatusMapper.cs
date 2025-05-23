using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Application.Interfaces.Mappers;

internal interface IMaritalStatusMapper
{
	MaritalStatusDto Map(MaritalStatus value);
}

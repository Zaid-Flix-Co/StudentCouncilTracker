using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.Application.DynamicFields.Mappers;

public class AfterMapEntityToEntityDtoData(IUserProvider userProvider) : AfterMapEntityToEntityDtoDataBase<object, object>(userProvider)
{
}
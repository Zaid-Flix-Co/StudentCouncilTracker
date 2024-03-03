using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services.Catalogs.UserRoles;

public class UserRoleService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "UserRole";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<UserRoleDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<UserRoleDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }
}
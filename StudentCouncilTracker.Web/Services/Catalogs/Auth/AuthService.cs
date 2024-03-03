using StudentCouncilTracker.Application.Entities.Tokens.Dto;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Web.Services.Catalogs.Auth;

public class AuthService(IHttpClientFactory clientFactory) : BaseCatalogService(clientFactory)
{
    protected override string BasePath => "Auth";

    public async Task<OperationResult<TokenDto>> LoginAsync(CatalogUserDto user)
    {
        var result = await SendAsync<TokenDto>("Login", HttpMethod.Post, user.Data);
        return result;
    }
}
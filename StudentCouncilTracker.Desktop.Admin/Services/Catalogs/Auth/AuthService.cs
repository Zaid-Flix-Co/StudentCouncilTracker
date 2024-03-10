using System.Net.Http;
using StudentCouncilTracker.Application.Entities.Tokens.Dto;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Auth;

public class AuthService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "Auth";

    public async Task<OperationResult<TokenDto>> LoginAsync(CatalogUserDto user)
    {
        var result = await SendAsync<TokenDto>("Login", HttpMethod.Post, user.Data);
        return result;
    }
}
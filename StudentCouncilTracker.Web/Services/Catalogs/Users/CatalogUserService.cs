using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services.Catalogs.Users;

public class CatalogUserService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "CatalogUser";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<CatalogUserDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<CatalogUserDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<CatalogUserDto>> GetEmptyAsync()
    {
        var result = await SendAsync<CatalogUserDto>($"GetEmpty", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<CatalogUserDtoJournal>> GetJournalAsync()
    {
        var result = await SendAsync<CatalogUserDtoJournal>("GetJournal", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<CatalogUserDto>> CreateAsync()
    {
        var result = await SendAsync<CatalogUserDto>("Create", HttpMethod.Post);
        return result;
    }

    public async Task<OperationResult<CatalogUserDto>> PutCardAsync(CatalogUserDto model)
    {
        var result = await SendAsync<CatalogUserDto>($"{model.Data.Id}", HttpMethod.Put, model.Data);
        return result;
    }

    public async Task<OperationResult<CatalogUserDto>> DeleteAsync(CatalogUserDtoJournalItem model)
    {
        var result = await SendAsync<CatalogUserDto>($"{model.Id}", HttpMethod.Delete);
        return result;
    }
}
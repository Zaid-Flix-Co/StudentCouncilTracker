using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services.Catalogs;

public abstract class BaseCatalogService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseService(clientFactory, userProvider)
{
    public Task<OperationResult<ListDto>> GetListAsync(string query = "")
    {
        return GetListAsync(query, new Dictionary<string, string>());
    }

    public virtual Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        return Task.FromResult(new OperationResult<ListDto>());
    }

    public Dictionary<string, string> GetParameters(string query, Dictionary<string, string> parameters = null)
    {
        parameters ??= new Dictionary<string, string>();

        parameters.TryAdd("query", query);
        parameters.TryAdd("page", "1");

        return parameters;
    }
}
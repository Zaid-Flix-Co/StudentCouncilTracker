using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services.Catalogs.EventActionStatuses;

public class EventActionStatusService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "EventActionStatus";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<EventActionStatusDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<EventActionStatusDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }
}
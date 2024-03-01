using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.Catalogs;

namespace StudentCouncilTracker.Web.Services;

public class EventTypeService(IHttpClientFactory clientFactory) : BaseCatalogService(clientFactory)
{
    protected override string BasePath => "EventType";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var ret = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return ret;
    }

    public async Task<OperationResult<EventTypeDto>> GetCardAsync(int id)
    {
        var dto = await SendAsync<EventTypeDto>($"Get/{id}", HttpMethod.Get);
        return dto;
    }
}
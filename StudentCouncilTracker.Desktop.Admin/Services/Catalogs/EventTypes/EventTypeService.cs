using System.Net.Http;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventTypes;

public class EventTypeService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "EventType";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<EventTypeDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<EventTypeDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }
}
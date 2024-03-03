using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services.Catalogs.Events;

public class EventService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "Event";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<EventDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<EventDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<EventDtoJournal>> GetJournalAsync()
    {
        var result = await SendAsync<EventDtoJournal>("GetJournal", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<EventDto>> CreateAsync()
    {
        var result = await SendAsync<EventDto>("Create", HttpMethod.Post);
        return result;
    }

    public async Task<OperationResult<EventDto>> PutCardAsync(EventDto model)
    {
        var result = await SendAsync<EventDto>($"{model.Data.Id}", HttpMethod.Put, model.Data);
        return result;
    }

    public async Task<OperationResult<EventDto>> DeleteAsync(EventDtoJournalItem model)
    {
        var result = await SendAsync<EventDto>($"{model.Id}", HttpMethod.Delete);
        return result;
    }
}
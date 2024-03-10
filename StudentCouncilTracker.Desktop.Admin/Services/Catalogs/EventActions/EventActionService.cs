using System.Net.Http;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventActions;

public class EventActionService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseCatalogService(clientFactory, userProvider)
{
    protected override string BasePath => "EventAction";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<EventActionDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<EventActionDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<EventActionDtoJournal>> GetJournalByEventIdAsync(int eventId)
    {
        var result = await SendAsync<EventActionDtoJournal>($"GetJournalByEventId/{eventId}", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<EventActionDtoJournal>> GetJournalByUserIdAsync(int userId)
    {
        var result = await SendAsync<EventActionDtoJournal>($"GetJournalByUserId/{userId}", HttpMethod.Get);
        return result;
    }

    public async Task<OperationResult<EventActionDto>> CreateAsync(int eventId)
    {
        var result = await SendAsync<EventActionDto>("Create", HttpMethod.Post, eventId);
        return result;
    }

    public async Task<OperationResult<EventActionDto>> PutCardAsync(EventActionDto model)
    {
        var result = await SendAsync<EventActionDto>($"{model.Data.Id}", HttpMethod.Put, model.Data);
        return result;
    }

    public async Task<OperationResult<EventActionDto>> DeleteAsync(EventActionDtoJournalItem model)
    {
        var result = await SendAsync<EventActionDto>($"{model.Id}", HttpMethod.Delete);
        return result;
    }
}
﻿using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Web.Services.Catalogs.EventActionTypes;

public class EventActionTypeService(IHttpClientFactory clientFactory) : BaseCatalogService(clientFactory)
{
    protected override string BasePath => "EventActionType";

    public override async Task<OperationResult<ListDto>> GetListAsync(string query, Dictionary<string, string> parameters)
    {
        var result = await SendAsync<ListDto>("GetList", HttpMethod.Post, GetParameters(query, parameters));
        return result;
    }

    public async Task<OperationResult<EventActionTypeDto>> GetCardAsync(int id)
    {
        var result = await SendAsync<EventActionTypeDto>($"Get/{id}", HttpMethod.Get);
        return result;
    }
}
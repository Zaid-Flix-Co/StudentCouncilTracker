﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Features.Events.Commands.Create;
using StudentCouncilTracker.Application.Features.Events.Commands.Delete;
using StudentCouncilTracker.Application.Features.Events.Commands.Update;
using StudentCouncilTracker.Application.Features.Events.Queries.Get;
using StudentCouncilTracker.Application.Features.Events.Queries.GetById;
using StudentCouncilTracker.Application.Features.Events.Queries.GetJournal;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.API.Controllers;

public class EventController(IUserProvider userProvider) : BaseController(userProvider)
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventByIdQuery(id, UserName, Role)));
    }
    
    [AllowAnonymous]
    [HttpPost("GetJournal")]
    public async Task<ActionResult<BaseResponseActionResult<EventDtoJournal>>> GetJournal([FromBody] bool isChecked)
    {
        var result = await Mediator.Send(new GetEventJournalQuery(UserName, Role, isChecked));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetEventQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<EventDto>>> Create()
    {
        var result = await Mediator.Send(new CreateEventCommand(UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<OperationResult<EventDto>>> Put(int id, [FromBody] EventDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventCommand(id));
        return Ok(result);
    }
}
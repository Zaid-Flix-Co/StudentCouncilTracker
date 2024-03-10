using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Features.EventActions.Commands.Create;
using StudentCouncilTracker.Application.Features.EventActions.Commands.Delete;
using StudentCouncilTracker.Application.Features.EventActions.Commands.Update;
using StudentCouncilTracker.Application.Features.EventActions.Queries.Get;
using StudentCouncilTracker.Application.Features.EventActions.Queries.GetById;
using StudentCouncilTracker.Application.Features.EventActions.Queries.GetJournal;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.API.Controllers;

public class EventActionController(IUserProvider userProvider) : BaseController(userProvider)
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventActionDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventActionByIdQuery(id)));
    }
    
    [AllowAnonymous]
    [HttpGet("GetJournalByEventId/{eventId:int}")]
    public async Task<ActionResult<BaseResponseActionResult<EventActionDtoJournal>>> GetJournalByEventId(int eventId)
    {
        var result = await Mediator.Send(new GetEventActionJournalQuery(true, eventId, UserName, Role));
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("GetJournalByUserId/{userId:int}")]
    public async Task<ActionResult<BaseResponseActionResult<EventActionDtoJournal>>> GetJournalByUserId(int userId)
    {
        var result = await Mediator.Send(new GetEventActionJournalQuery(false, userId, UserName, Role));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetEventActionQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<EventActionDto>>> Create([FromBody] int eventId)
    {
        var result = await Mediator.Send(new CreateEventActionCommand(eventId, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<OperationResult<EventActionDto>>> Put(int id, [FromBody] EventActionDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventActionCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventActionCommand(id));
        return Ok(result);
    }
}
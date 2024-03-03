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

namespace StudentCouncilTracker.API.Controllers;

public class EventActionController : BaseController
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventActionDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventActionByIdQuery(id)));
    }
    
    [AllowAnonymous]
    [HttpGet("GetJournal/{eventId:int}")]
    public async Task<ActionResult<BaseResponseActionResult<EventActionDtoJournal>>> GetJournal(int eventId)
    {
        var result = await Mediator.Send(new GetEventActionJournalQuery(eventId));
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
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<EventActionDto>>> Put(int id, [FromBody] EventActionDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventActionCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventActionCommand(id));
        return Ok(result);
    }
}
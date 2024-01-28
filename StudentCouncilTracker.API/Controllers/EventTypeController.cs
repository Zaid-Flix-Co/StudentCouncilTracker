using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Features.EventTypes.Commands.CreateEventType;
using StudentCouncilTracker.Application.Features.EventTypes.Commands.DeleteEventType;
using StudentCouncilTracker.Application.Features.EventTypes.Commands.UpdateEventType;
using StudentCouncilTracker.Application.Features.EventTypes.Queries.Get;
using StudentCouncilTracker.Application.Features.EventTypes.Queries.GetById;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.API.Controllers;

public class EventTypeTypeController : BaseController
{
    [AllowAnonymous]
    [HttpPost("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventTypeDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventTypeByIdQuery(id)));
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetEventTypeQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<EventTypeDto>>> Create()
    {
        var result = await Mediator.Send(new CreateEventTypeCommand());
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<EventTypeDto>>> Put(int id, [FromBody] EventTypeDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventTypeCommand(id, data));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventTypeCommand(id));
        return Ok(result);
    }
}
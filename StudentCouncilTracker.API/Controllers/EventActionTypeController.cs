using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Dto;
using StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Create;
using StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Delete;
using StudentCouncilTracker.Application.Features.EventActionTypes.Commands.Update;
using StudentCouncilTracker.Application.Features.EventActionTypes.Queries.Get;
using StudentCouncilTracker.Application.Features.EventActionTypes.Queries.GetById;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.API.Controllers;

public class EventActionTypeController : BaseController
{
    [AllowAnonymous]
    [HttpPost("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventActionTypeDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventActionTypeByIdQuery(id)));
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetEventActionTypeQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<EventActionTypeDto>>> Create()
    {
        var result = await Mediator.Send(new CreateEventActionTypeCommand(UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<EventActionTypeDto>>> Put(int id, [FromBody] EventActionTypeDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventActionTypeCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventActionTypeCommand(id));
        return Ok(result);
    }
}
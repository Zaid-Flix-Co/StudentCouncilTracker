using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto;
using StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Create;
using StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Delete;
using StudentCouncilTracker.Application.Features.EventActionStatuses.Commands.Update;
using StudentCouncilTracker.Application.Features.EventActionStatuses.Queries.Get;
using StudentCouncilTracker.Application.Features.EventActionStatuses.Queries.GetById;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.API.Controllers;

public class EventActionStatusController : BaseController
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<EventActionStatusDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetEventActionStatusByIdQuery(id)));
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetEventActionStatusQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<EventActionStatusDto>>> Create()
    {
        var result = await Mediator.Send(new CreateEventActionStatusCommand(UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<EventActionStatusDto>>> Put(int id, [FromBody] EventActionStatusDtoData data)
    {
        var result = await Mediator.Send(new UpdateEventActionStatusCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteEventActionStatusCommand(id));
        return Ok(result);
    }
}
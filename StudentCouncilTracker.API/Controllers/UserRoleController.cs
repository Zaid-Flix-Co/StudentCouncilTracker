using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.UserRoles.Dto;
using StudentCouncilTracker.Application.Features.UserRoles.Commands.Create;
using StudentCouncilTracker.Application.Features.UserRoles.Commands.Delete;
using StudentCouncilTracker.Application.Features.UserRoles.Commands.Update;
using StudentCouncilTracker.Application.Features.UserRoles.Queries.Get;
using StudentCouncilTracker.Application.Features.UserRoles.Queries.GetById;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.API.Controllers;

public class UserRoleController : BaseController
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<UserRoleDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetUserRoleByIdQuery(id)));
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetUserRoleQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<UserRoleDto>>> Create()
    {
        var result = await Mediator.Send(new CreateUserRoleCommand(UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<UserRoleDto>>> Put(int id, [FromBody] UserRoleDtoData data)
    {
        var result = await Mediator.Send(new UpdateUserRoleCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteUserRoleCommand(id));
        return Ok(result);
    }
}
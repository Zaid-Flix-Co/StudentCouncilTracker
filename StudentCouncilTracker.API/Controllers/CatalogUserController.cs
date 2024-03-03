using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudentCouncilTracker.API.Models.ActionResults;
using StudentCouncilTracker.Application.Entities.Base.Dto;
using StudentCouncilTracker.Application.Entities.Base.Filters;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Entities.Users.Interfaces;
using StudentCouncilTracker.Application.Features.Users.Commands.Create;
using StudentCouncilTracker.Application.Features.Users.Commands.Delete;
using StudentCouncilTracker.Application.Features.Users.Commands.Update;
using StudentCouncilTracker.Application.Features.Users.Queries.Get;
using StudentCouncilTracker.Application.Features.Users.Queries.GetById;
using StudentCouncilTracker.Application.Features.Users.Queries.GetEmpty;
using StudentCouncilTracker.Application.Features.Users.Queries.GetJournal;
using StudentCouncilTracker.Application.FileSavers;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.Hubs.Progress;

namespace StudentCouncilTracker.API.Controllers;

public class CatalogUserController(IHubContext<HubProgress, IHubProgress> hubProgress, FileSaverService fileSaverService, IHostEnvironment env, ICatalogUserRepository catalogUserRepository) : BaseController
{
    [AllowAnonymous]
    [HttpGet("Get/{id:int}")]
    public async Task<BaseResponseActionResult<CatalogUserDto>> Get(int id)
    {
        return Ok(await Mediator.Send(new GetUserByIdQuery(id)));
    }    
    
    [AllowAnonymous]
    [HttpGet("GetEmpty")]
    public async Task<BaseResponseActionResult<CatalogUserDto>> GetEmpty()
    {
        return Ok(await Mediator.Send(new GetUserEmptyQuery()));
    }

    [AllowAnonymous]
    [HttpGet("GetJournal")]
    public async Task<ActionResult<BaseResponseActionResult<CatalogUserDtoJournal>>> GetJournal()
    {
        var result = await Mediator.Send(new GetCatalogUserJournalQuery());
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("GetList")]
    public async Task<BaseResponseActionResult<ListDto>> GetList([FromBody] ListFilter data)
    {
        return Ok(await Mediator.Send(new GetUserQuery(data)));
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<ActionResult<OperationResult<CatalogUserDto>>> Create()
    {
        var result = await Mediator.Send(new CreateUserCommand(UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationResult<CatalogUserDto>>> Put(int id, [FromBody] CatalogUserDtoData data)
    {
        var result = await Mediator.Send(new UpdateUserCommand(id, data, UserName));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id));
        return Ok(result);
    }
}
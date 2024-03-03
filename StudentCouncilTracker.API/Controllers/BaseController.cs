using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace StudentCouncilTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

    protected string UserName
    {
        get
        {
            var userNameHeaderValue = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "UserName").Value;
            var decodedUserName = Encoding.UTF8.GetString(Convert.FromBase64String(userNameHeaderValue));
            return decodedUserName;
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Services.UserProviders;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StudentCouncilTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseController(IUserProvider userProvider) : ControllerBase
{
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

    protected string UserName
    {
        get
        {
            var userNameHeaderValue = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "UserName").Value;
            if(!userNameHeaderValue.IsNullOrEmpty())
            {
                var decodedUserName = Encoding.UTF8.GetString(Convert.FromBase64String(userNameHeaderValue!));

                userProvider.Name = decodedUserName;
                return decodedUserName;
            }

            return string.Empty;
        }
    }

    protected Role Role
    {
        get
        {
            var roleHeaderValue = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "Role").Value;

            if (Enum.TryParse(typeof(Role), roleHeaderValue.ToString(), out var enumValue))
            {
                userProvider.Role = (Role)enumValue;
                return (Role)enumValue;
            }

            userProvider.Role = Role.None;
            return Role.None;
        }
    }
}
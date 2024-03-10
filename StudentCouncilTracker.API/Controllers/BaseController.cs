using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Services.UserProviders;
using System.Text;

namespace StudentCouncilTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseController(IUserProvider userProvider) : ControllerBase
{
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

    private const string UserNameClaim = "UserName";

    private const string RoleClaim = "Role";

    protected string UserName
    {
        get
        {
            var userNameHeaderValue = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == UserNameClaim).Value;

            if (userNameHeaderValue.IsNullOrEmpty()) 
                return string.Empty;

            var decodedUserName = Encoding.UTF8.GetString(Convert.FromBase64String(userNameHeaderValue!));

            userProvider.Name = decodedUserName;
            return decodedUserName;

        }
    }

    protected Role Role
    {
        get
        {
            var roleHeaderValue = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == RoleClaim).Value;

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
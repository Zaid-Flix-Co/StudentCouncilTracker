using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.Tokens.Dto;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Features.Users.Queries.GetByLogin;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.UserProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCouncilTracker.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController(IUserProvider userProvider) : BaseController(userProvider)
{
    private const char Separator = ',';

    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<TokenDto>> Login([FromBody] CatalogUserDtoData data)
    {
        var operationResult = new OperationResult<TokenDto>();
        var user = await Mediator.Send(new GetUserByLoginQuery(data));

        if (user.Ok)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("001E33952B8942A5B07D88ECD3CD4719001E33952B8942A5B07D88ECD3CD4719"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Value.Data.Name?.Value!),
                new Claim("Role", user.Value.Data.Role?.Value.Id.ToString() ?? Role.Member.ToString()),
                new Claim("UserId", user.Value.Data.Id.ToString())
            };
            
            var tokenOptions = new JwtSecurityToken(
                issuer: "Polina",
                audience: "https://localhost:7090",
                claims: [..claims],
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            operationResult.SetValue(new TokenDto(tokenString));
            return operationResult;
        }
        else
        {
            operationResult.AddError(string.Join(Separator, user.Reasons.Select(reason => reason.Message)));
            return operationResult;
        }
    }
}
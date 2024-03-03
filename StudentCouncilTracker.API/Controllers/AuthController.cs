using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.Tokens.Dto;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Application.Features.Users.Queries.GetByLogin;
using StudentCouncilTracker.Application.OperationResults;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCouncilTracker.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : BaseController
{
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
            };
            
            var tokenOptions = new JwtSecurityToken(
                issuer: "Polina",
                audience: "https://localhost:7090",
                claims: [..claims],
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            operationResult.SetValue(new TokenDto { AccessToken = tokenString });

            Response.Cookies.Append("AccessToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return operationResult;
        }
        else
        {
            operationResult.AddError("Неверно введен логин или пароль");
            return operationResult;
        }
    }
}
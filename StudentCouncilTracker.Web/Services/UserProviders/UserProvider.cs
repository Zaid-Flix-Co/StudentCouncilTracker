using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentCouncilTracker.Web.Services.UserProviders;

public class UserProvider : IUserProvider
{
    public required string Name { get; set; }

    public Role Role { get; set; }

    public required int UserId { get; set; }

    public void ParseJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
        {
            var claims = jsonToken.Claims;

            var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (nameClaim != null)
                Name = nameClaim.Value;

            var roleClaim = claims.FirstOrDefault(c => c.Type == "Role");

            if (roleClaim != null)
                if (Enum.TryParse(typeof(Role), roleClaim.Value, out var enumValue))
                {
                    Role = (Role)enumValue;
                }

            var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim != null)
                UserId = Convert.ToInt32(userIdClaim.Value);
        }
    }
}
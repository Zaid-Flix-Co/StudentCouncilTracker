using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentCouncilTracker.Web.Services.UserProviders;

public class UserProvider : IUserProvider
{
    public string Name { get; set; }

    public void ParseJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
        {
            var claims = jsonToken.Claims;

            var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (nameClaim != null)
                Name = nameClaim.Value;
        }
    }
}
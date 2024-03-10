namespace StudentCouncilTracker.Application.Entities.Tokens.Dto;

public class TokenDto
{
    public TokenDto()
    {

    }

    public TokenDto(string accessToken)
    {
        AccessToken = accessToken;
    }
    
    public string AccessToken { get; set; }
}
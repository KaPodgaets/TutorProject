namespace Users.Application.Models;

public record JwtResult(string AccessToken, Guid AccessTokenJti);
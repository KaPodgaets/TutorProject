namespace Users.Contracts.Responses;

public record LoginResponse(string AccessToken, Guid RefreshToken);
namespace TutorProject.Application.Models;

public record JwtResult(string AccessToken, Guid AccessTokenJti);
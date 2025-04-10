using System.Security.Claims;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Users.Application.Models;
using Users.Domain;

namespace Users.Application.Abstractions;

public interface ITokenProvider
{
    Task<JwtResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default);

    Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<Claim>, ErrorList>> GetUserClaims(
        string token,
        CancellationToken cancellationToken = default);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using Shared.ResultPattern;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using TutorProject.Application.Models;
using Users.Domain;
using Users.Infrastructure.Postgres.DbContext;
using Users.Infrastructure.Postgres.Options;

namespace Users.Infrastructure.Postgres.JwtProvider;

public class TokenTokenProvider : ITokenProvider
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly UsersDbContext _usersDbContext;
    private readonly IRolesRepository _permissionManager;
    private readonly ILogger<TokenTokenProvider> _logger;

    public TokenTokenProvider(
        IOptions<JwtOptions> jwtOptions,
        ILogger<TokenTokenProvider> logger,
        UsersDbContext usersDbContext,
        IRolesRepository permissionManager)
    {
        _jwtOptions = jwtOptions;
        _logger = logger;
        _usersDbContext = usersDbContext;
        _permissionManager = permissionManager;
    }

    public async Task<JwtResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name));

        var permissionCodes = await _permissionManager
            .GetPermissionCodesByUserId(user.Id, cancellationToken);
        var permissionClaims = permissionCodes
            .Select(x => new Claim(CustomClaims.Permission, x));

        var jti = Guid.NewGuid();

        Claim[] claims =
        [
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            expires: DateTime.Now.AddMinutes(double.Parse(_jwtOptions.Value.Expiration)),
            signingCredentials: signingCredentials,
            claims: claims);

        string? stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        _logger.LogInformation("{user} successfully logged in", user.Email);

        return new JwtResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(30),
            UserId = user.Id,
            RefreshToken = Guid.NewGuid(),
            Jti = jti
        };

        _usersDbContext.Add(refreshSession);
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>, ErrorList>> GetUserClaims(
        string token,
        CancellationToken cancellationToken = default)
    {
        var validationParameters = JwtValidationParametersFactory.CreateWithoutLifeTime(_jwtOptions.Value);
        var jwtHandler = new JwtSecurityTokenHandler();
        var result = await jwtHandler.ValidateTokenAsync(token, validationParameters);
        if (result.IsValid is false)
            return Errors.Tokens.NotValid().ToErrorList();

        return result.ClaimsIdentity.Claims.ToList();
    }
}
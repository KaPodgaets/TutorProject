using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using TutorProject.Application.Abstractions;

namespace TutorProject.Application.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponseModel, LoginCommand>
{
    private readonly LoginValidator _validator;
    private readonly IUserManager _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        LoginValidator validator,
        ILogger<LoginHandler> logger,
        IUserManager userManager,
        ITokenProvider tokenProvider)
    {
        _validator = validator;
        _logger = logger;
        _userManager = userManager;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<LoginResponseModel, ErrorList>> ExecuteAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation inputs
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        // create new domain entity
        var email = Email.Create(command.Email).Value;

        var loginResult = await _userManager.LoginAsync(email, command.Password, cancellationToken);
        if (loginResult.IsFailure)
            return loginResult.Error;

        _logger.LogInformation("User {UserId} created", loginResult.Value);

        // token logic
        var jwtResult = await _tokenProvider.GenerateAccessToken(loginResult.Value, cancellationToken);
        var refreshToken = await _tokenProvider.GenerateRefreshToken(loginResult.Value, jwtResult.AccessTokenJti, cancellationToken);

        var responseModel = new LoginResponseModel(jwtResult.AccessToken, refreshToken);
        return responseModel;
    }
}

public record LoginResponseModel(string AccessToken, Guid RefreshToken);
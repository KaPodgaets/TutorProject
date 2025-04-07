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
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        LoginValidator validator,
        ILogger<LoginHandler> logger,
        IUserManager userManager)
    {
        _validator = validator;
        _logger = logger;
        _userManager = userManager;
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

        // business logic validation

        // create new domain entity
        var email = Email.Create(command.Email).Value;

        var loginResult = await _userManager.LoginAsync(email, command.Password, cancellationToken);
        if (loginResult.IsFailure)
            return loginResult.Error;

        _logger.LogInformation("User {UserId} created", loginResult.Value);

        // TODO token provider logic
        string accessToken = "test token";
        string refreshToken = "refresh test token";
        var responseModel = new LoginResponseModel(accessToken, refreshToken);
        return responseModel;
    }
}

public record LoginResponseModel(string AccessToken, string RefreshToken);
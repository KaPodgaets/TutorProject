using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using TutorProject.Application.Commands.CreateUser;

namespace TutorProject.Application.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Email)));
        RuleFor(x => x.Password).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Password)));

        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
    }
}
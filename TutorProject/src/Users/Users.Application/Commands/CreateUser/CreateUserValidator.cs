using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;

namespace TutorProject.Application.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Email)));
        RuleFor(x => x.Password).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Password)));

        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
        RuleFor(x => x.Password).MustBeValueObject(Password.Create);
    }
}
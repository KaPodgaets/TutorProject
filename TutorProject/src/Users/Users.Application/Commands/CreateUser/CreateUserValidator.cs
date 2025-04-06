using FluentValidation;
using Shared.Errors;

namespace TutorProject.Application.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Email)));
        RuleFor(x => x.Password).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Password)));
    }
}
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TutorProject.Domain.Shared.Errors;

namespace TutorProject.Application.Users;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Email)));
        RuleFor(x => x.Password).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateUserCommand.Password)));
    }
}
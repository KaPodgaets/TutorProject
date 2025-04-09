using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.FirstName)));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.LastName)));

        RuleFor(x => new { x.FirstName, x.LastName })
            .Must(x =>
            {
                var fullNameResult = FullName.Create(x.FirstName, x.LastName);
                return fullNameResult.IsSuccess;
            })
            .WithError(Errors.General.ValueIsInvalid(nameof(FullName)));
    }
}
using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Application.Commands.CreateStudent;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Commands.UpdateStudent;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(UpdateStudentCommand.StudentId)));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.FirstName)));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.LastName)));

        RuleFor(x => new { x.FirstName, x.LastName })
            .Must(x => FullName.Create(x.FirstName, x.LastName).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid(nameof(FullName)));

        RuleFor(x => x.CitizenId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.CitizenId)));

        RuleFor(x => x.CitizenId)
            .Must(id => Students.Domain.Students.ValueObjects.CitizenId.Create(id).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid(nameof(CreateStudentCommand.CitizenId)));

        // Passport is optional; validate only if provided
        When(
            x => !string.IsNullOrWhiteSpace(x.PassportNumber) || !string.IsNullOrWhiteSpace(x.PassportCountry),
            () =>
            {
                RuleFor(x => x.PassportNumber)
                    .NotEmpty()
                    .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.PassportNumber)));

                RuleFor(x => x.PassportCountry)
                    .NotEmpty()
                    .WithError(Errors.General.ValueIsRequired(nameof(CreateStudentCommand.PassportCountry)));

                RuleFor(x => new { x.PassportNumber, x.PassportCountry })
                    .Must(x => Passport.Create(x.PassportNumber, x.PassportCountry).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Passport"));
            });

        RuleFor(x => x.SchoolId)
            .Must(id => id == null || id != Guid.Empty)
            .WithError(Errors.General.ValueIsInvalid(nameof(CreateStudentCommand.SchoolId)));
    }
}
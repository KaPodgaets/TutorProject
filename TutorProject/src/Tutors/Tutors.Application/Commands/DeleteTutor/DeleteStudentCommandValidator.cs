using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;

namespace Tutors.Application.Commands.DeleteTutor;

public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).NotNull()
            .WithError(Errors.General.ValueIsRequired(nameof(StudentId)));
    }
}
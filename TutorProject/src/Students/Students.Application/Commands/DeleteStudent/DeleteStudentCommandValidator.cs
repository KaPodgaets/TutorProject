using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Domain.Students.Ids;

namespace Students.Application.Commands.DeleteStudent;

public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).NotNull()
            .WithError(Errors.General.ValueIsRequired(nameof(StudentId)));
    }
}
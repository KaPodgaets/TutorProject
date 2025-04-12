using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Tutors.Domain;

namespace Tutors.Application.Commands.DeleteTutor;

public class DeleteTutorCommandValidator : AbstractValidator<DeleteTutorCommand>
{
    public DeleteTutorCommandValidator()
    {
        RuleFor(x => x.TutorId).NotNull()
            .WithError(Errors.General.ValueIsRequired(nameof(TutorId)));
    }
}
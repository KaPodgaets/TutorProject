using FluentValidation;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using Tutors.Application.Commands.CreateTutor;
using Tutors.Domain;

namespace Tutors.Application.Commands.UpdateTutor;

public class UpdateTutorCommandValidator : AbstractValidator<UpdateTutorCommand>
{
    public UpdateTutorCommandValidator()
    {
        RuleFor(command => command.TutorId).NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(TutorId)));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateTutorCommand.FirstName)));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired(nameof(CreateTutorCommand.LastName)));

        RuleFor(x => new { x.FirstName, x.LastName })
            .Must(x => FullName.Create(x.FirstName, x.LastName).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid(nameof(FullName)));

        RuleFor(x => x.CitizenId)
            .MustBeValueObject(CitizenId.Create);

        RuleFor(x => x.Address)
            .MustBeValueObject(dto =>
                Address.Create(
                    dto.StreetCode,
                    dto.StreetName,
                    dto.CityCode,
                    dto.CityName,
                    dto.BuildingNumber,
                    dto.BuildingLetter));

        RuleFor(x => x.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(x => x.Email)
            .MustBeValueObject(Email.Create);
    }
}
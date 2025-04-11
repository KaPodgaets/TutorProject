using FluentValidation;
using Shared.Validation;
using Shared.ValueObjects;

namespace Tutors.Application.Commands.CreateTutor;

public class CreateTutorCommandValidator : AbstractValidator<CreateTutorCommand>
{
    public CreateTutorCommandValidator()
    {
        RuleFor(x => x.Address)
            .MustBeValueObject(dto =>
                Address.Create(
                    dto.StreetCode,
                    dto.StreetName,
                    dto.CityCode,
                    dto.CityName,
                    dto.BuildingNumber,
                    dto.BuildingLetter));
    }
}
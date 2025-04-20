using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using Tutors.Application.Database;
using Tutors.Domain;

namespace Tutors.Application.Commands.CreateTutor;

public class CreateTutorHandler : ICommandHandler<Guid, CreateTutorCommand>
{
    private readonly ITutorsRepository _repository;
    private readonly CreateTutorCommandValidator _validator;
    private readonly ILogger<CreateTutorHandler> _logger;

    public CreateTutorHandler(
        ILogger<CreateTutorHandler> logger,
        ITutorsRepository repository,
        CreateTutorCommandValidator validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        CreateTutorCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation inputs
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        // business logic validation
        // TODO - check that same FullName already exists - notification only in UI, without error in back-end
        // TODO - check that citizenId already exists

        // create new domain entity
        var fullName = FullName.Create(command.FirstName, command.LastName).Value;

        CitizenId citizenId = CitizenId.Create(command.CitizenId).Value;

        Address address = Address.Create(
            command.Address.StreetCode,
            command.Address.StreetName,
            command.Address.CityCode,
            command.Address.CityName,
            command.Address.BuildingNumber,
            command.Address.BuildingLetter).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var email = Email.Create(command.Email).Value;

        var newTutorModel = Tutor.Create(
            TutorId.NewTutorId(),
            fullName,
            citizenId,
            address,
            email,
            phoneNumber);

        if (newTutorModel.IsFailure)
            return newTutorModel.Error;

        var saveNewTutorResult = await _repository.Create(newTutorModel.Value, cancellationToken);
        if (saveNewTutorResult.IsFailure)
            return saveNewTutorResult.Error;

        _logger.LogInformation("Student with id: {StudentId} created", saveNewTutorResult.Value);

        return saveNewTutorResult.Value;
    }
}
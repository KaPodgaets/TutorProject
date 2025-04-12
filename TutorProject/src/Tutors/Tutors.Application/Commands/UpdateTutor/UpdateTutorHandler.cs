using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using Tutors.Application.Commands.CreateTutor;
using Tutors.Application.Database;
using Tutors.Domain;

namespace Tutors.Application.Commands.UpdateTutor;

public class UpdateTutorHandler : ICommandHandler<Guid, UpdateTutorCommand>
{
    private readonly ITutorsRepository _repository;
    private readonly UpdateTutorCommandValidator _validator;
    private readonly ILogger<CreateTutorHandler> _logger;

    public UpdateTutorHandler(
        ITutorsRepository repository,
        UpdateTutorCommandValidator validator,
        ILogger<CreateTutorHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        UpdateTutorCommand command,
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
        // TODO - check that passport already exists

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

        var saveChangesResult = await _repository.Update(newTutorModel.Value, cancellationToken);
        if (saveChangesResult.IsFailure)
            return saveChangesResult.Error;

        _logger.LogInformation("Student with id: {StudentId} created", saveChangesResult.Value);

        return saveChangesResult.Value;
    }
}
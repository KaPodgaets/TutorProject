using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using Tutors.Application.Database;

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
        // TODO - check that passport already exists

        // create new domain entity
        var fullName = FullName.Create(command.FirstName, command.LastName).Value;

        CitizenId citizenId = string.IsNullOrWhiteSpace(command.CitizenId)
            ? CitizenId.Create(command.CitizenId).Value
            : CitizenId.None;

        Passport passport = (command.PassportNumber, command.PassportCountry) is
            (not null, not null)
                ? Passport.Create(command.PassportNumber, command.PassportCountry).Value
                : Passport.None;

        var newStudentModel = Student.Create(
            StudentId.NewStudentId(),
            fullName,
            citizenId,
            passport,
            command.SchoolId);

        if (newStudentModel.IsFailure)
            return newStudentModel.Error;

        var createNewStudentResult = await _repository.Create(newStudentModel.Value, cancellationToken);
        if (createNewStudentResult.IsFailure)
            return createNewStudentResult.Error;

        _logger.LogInformation("Student with id: {StudentId} created", createNewStudentResult.Value);

        return createNewStudentResult.Value;
    }
}
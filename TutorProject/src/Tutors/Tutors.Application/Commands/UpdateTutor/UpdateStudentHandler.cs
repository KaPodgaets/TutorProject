using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using Tutors.Application.Commands.CreateTutor;
using Tutors.Application.Database;

namespace Tutors.Application.Commands.UpdateTutor;

public class UpdateStudentHandler : ICommandHandler<Guid, UpdateStudentCommand>
{
    private readonly ITutorsRepository _repository;
    private readonly UpdateStudentCommandValidator _validator;
    private readonly ILogger<CreateTutorHandler> _logger;

    public UpdateStudentHandler(
        ITutorsRepository repository,
        UpdateStudentCommandValidator validator,
        ILogger<CreateTutorHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        UpdateStudentCommand command,
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

        var studentId = StudentId.Create(command.StudentId).Value;
        var existingStudent = await _repository.GetById(studentId, cancellationToken);
        if (existingStudent.IsFailure)
            return existingStudent.Error;

        var updateResult = existingStudent.Value.Update(
            fullName,
            citizenId,
            passport,
            command.SchoolId);

        if (updateResult.IsFailure)
            return updateResult.Error;

        var saveChangesResult = await _repository.Update(existingStudent.Value, cancellationToken);
        if (saveChangesResult.IsFailure)
            return saveChangesResult.Error;

        _logger.LogInformation("Student with id: {StudentId} created", saveChangesResult.Value);

        return saveChangesResult.Value;
    }
}
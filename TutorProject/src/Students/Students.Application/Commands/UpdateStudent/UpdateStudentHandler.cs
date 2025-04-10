using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Application.Commands.CreateStudent;
using Students.Application.Database;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Commands.UpdateStudent;

public class UpdateStudentHandler : ICommandHandler<Guid, UpdateStudentCommand>
{
    private readonly IStudentsRepository _repository;
    private readonly UpdateStudentCommandValidator _validator;
    private readonly ILogger<CreateStudentHandler> _logger;

    public UpdateStudentHandler(
        IStudentsRepository repository,
        UpdateStudentCommandValidator validator,
        ILogger<CreateStudentHandler> logger)
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
        var citizenId = CitizenId.Create(command.CitizenId).Value;

        var passport = (command.PassportNumber, command.PassportCountry) is
            (not null, not null)
                ? Passport.Create(command.PassportNumber, command.PassportCountry).Value
                : null;

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
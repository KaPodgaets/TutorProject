using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Application.Database;
using Students.Domain.Students;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Commands.CreateStudent;

public class CreateStudentHandler : ICommandHandler<Guid, CreateStudentCommand>
{
    private readonly IStudentsRepository _repository;
    private readonly CreateStudentCommandValidator _validator;
    private readonly ILogger<CreateStudentHandler> _logger;

    public CreateStudentHandler(
        ILogger<CreateStudentHandler> logger,
        IStudentsRepository repository,
        CreateStudentCommandValidator validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        CreateStudentCommand command,
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

        _logger.LogInformation("Student with id: {UserId} created", createNewStudentResult.Value);

        return createNewStudentResult.Value;
    }
}
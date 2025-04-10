using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Students.Application.Commands.CreateStudent;
using Students.Application.Database;
using Students.Domain.Students;
using Students.Domain.Students.Ids;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Commands.DeleteStudent;

public class DeleteStudentHandler : ICommandHandler<Guid, DeleteStudentCommand>
{
    private readonly IStudentsRepository _repository;
    private readonly DeleteStudentCommandValidator _validator;
    private readonly ILogger<DeleteStudentHandler> _logger;

    public DeleteStudentHandler(
        IStudentsRepository repository,
        DeleteStudentCommandValidator validator,
        ILogger<DeleteStudentHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        DeleteStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation inputs
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var studentId = StudentId.Create(command.StudentId).Value;
        var existingStudent = await _repository.GetById(studentId, cancellationToken);
        if (existingStudent.IsFailure)
            return existingStudent.Error;

        // TODO - Business logic validation (for example that he is not assigned to tutor
        var deleteStudent = await _repository.Delete(existingStudent.Value, cancellationToken);
        if (deleteStudent.IsFailure)
            return deleteStudent.Error;

        _logger.LogInformation("Student with id: {StudentId} created", deleteStudent.Value);

        return deleteStudent.Value;
    }
}
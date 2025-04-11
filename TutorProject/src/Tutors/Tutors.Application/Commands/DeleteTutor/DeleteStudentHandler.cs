using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Tutors.Application.Database;

namespace Tutors.Application.Commands.DeleteTutor;

public class DeleteStudentHandler : ICommandHandler<Guid, DeleteStudentCommand>
{
    private readonly ITutorsRepository _repository;
    private readonly DeleteStudentCommandValidator _validator;
    private readonly ILogger<DeleteStudentHandler> _logger;

    public DeleteStudentHandler(
        ITutorsRepository repository,
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
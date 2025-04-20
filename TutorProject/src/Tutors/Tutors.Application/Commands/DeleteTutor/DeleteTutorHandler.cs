using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Tutors.Application.Database;
using Tutors.Domain;

namespace Tutors.Application.Commands.DeleteTutor;

public class DeleteTutorHandler : ICommandHandler<Guid, DeleteTutorCommand>
{
    private readonly ITutorsRepository _repository;
    private readonly DeleteTutorCommandValidator _validator;
    private readonly ILogger<DeleteTutorHandler> _logger;

    public DeleteTutorHandler(
        ITutorsRepository repository,
        DeleteTutorCommandValidator validator,
        ILogger<DeleteTutorHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        DeleteTutorCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation inputs
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var studentId = TutorId.Create(command.TutorId).Value;
        var existingStudent = await _repository.GetById(studentId, cancellationToken);
        if (existingStudent.IsFailure)
            return existingStudent.Error;

        // TODO - Business logic validation (for example that he does not have students
        var deleteStudent = await _repository.Delete(existingStudent.Value, cancellationToken);
        if (deleteStudent.IsFailure)
            return deleteStudent.Error;

        _logger.LogInformation("Student with id: {StudentId} created", deleteStudent.Value);

        return deleteStudent.Value;
    }
}
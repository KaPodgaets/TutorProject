using Shared.Abstractions;
using Tutors.Contracts.Dtos;

namespace Tutors.Application.Commands.UpdateTutor;

public record UpdateTutorCommand(
    string FirstName,
    string LastName,
    string CitizenId,
    AddressDto Address,
    string PhoneNumber,
    string Email) : ICommand;
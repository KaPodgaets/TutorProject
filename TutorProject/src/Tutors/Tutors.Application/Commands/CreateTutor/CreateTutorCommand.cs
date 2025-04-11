using Shared.Abstractions;
using Tutors.Contracts.Dtos;

namespace Tutors.Application.Commands.CreateTutor;

public record CreateTutorCommand(
    string FirstName,
    string LastName,
    string CitizenId,
    AddressDto Address,
    string PhoneNumber,
    string Email) : ICommand;
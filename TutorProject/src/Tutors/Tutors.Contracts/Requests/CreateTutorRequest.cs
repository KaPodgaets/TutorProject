using Tutors.Contracts.Dtos;

namespace Tutors.Contracts.Requests;

public record CreateTutorRequest(
    string FirstName,
    string LastName,
    string CitizenId,
    AddressDto Address,
    string PhoneNumber,
    string Email);
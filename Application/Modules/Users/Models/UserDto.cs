
namespace Application.Modules.Users.Models;
public record UserDto(
    Guid id, 
    string username,
    string email,
    string firstName,
    string lastName,
    DateTime dateOfBirth,
    string phoneNumber, 
    bool isActive,
    DateTime createdDate,
    DateTime lastModifiedDate, 
    string gender, 
    string street,
    string city,
    string state,
    string country,
    string postalCode);
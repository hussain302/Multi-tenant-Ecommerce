using Domain.Enums;
using Application.Responses;
using System.Text.RegularExpressions;
using Application.Abstractions.MediatR;
using MediatR;

namespace Application.Modules.Users.Commands;
public record UpdateUserCommand : ICommand<DomainResponse<Unit>>
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsActive { get; private set; }
    public string Gender { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? CompanyName { get; private set; }
    public string? Description { get; private set; }
    public string? ShippingAddress { get; private set; }

    public UpdateUserCommand(
        Guid id,
        string username,
        string email,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string phoneNumber,
        bool isActive,
        string gender,
        string street,
        string city,
        string state,
        string country,
        string? description,
        string? companyName,
        string? shippingAddress,
        string postalCode)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        IsActive = isActive;
        Gender = gender;
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        CompanyName = companyName;
        ShippingAddress = shippingAddress;
        Description = description;
        IsValidCommand = CheckValidity();
        IsValidEmail = CheckEmailValidity();
    }

    public bool IsValidCommand { get; private set; }
    public bool IsValidEmail { get; private set; }
    private bool CheckValidity()
       => !string.IsNullOrEmpty(Username) 
        && !string.IsNullOrEmpty(Email) 
        && !string.IsNullOrEmpty(FirstName)
        && !string.IsNullOrEmpty(PhoneNumber);
    private bool CheckEmailValidity() 
        => Regex.IsMatch(
            Email, 
            @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
}

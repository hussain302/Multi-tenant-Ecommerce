using Domain.Enums;
using Application.Responses;
using System.Text.RegularExpressions;
using Application.Abstractions.MediatR;
using Application.Modules.Roles.Models;

namespace Application.Modules.Users.Commands;

public partial record CreateVendorCommand : ICommand<DomainResponse<Guid>>
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Gender { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? CompanyName { get; private set; }
    public string? Description { get; private set; }
    public string? ShippingAddress { get; private set; }
    public Discriminator Discriminator { get; private set; }

    public CreateVendorCommand(
        string username,
        string email,
        string firstName,
        string lastName,
        string password,
        DateTime dateOfBirth,
        string phoneNumber,
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
        Discriminator = Discriminator.Vendor;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Password = password;
        PhoneNumber = phoneNumber;
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
        IsValidPassword = MeetsPasswordPolicy(password);
    }

    public bool IsValidCommand { get; private set; }
    public bool IsValidEmail { get; private set; }
    public bool IsValidPassword { get; private set; }
    private bool CheckValidity()
        => !string.IsNullOrEmpty(Username)
        && !string.IsNullOrEmpty(Email)
        && !string.IsNullOrEmpty(FirstName)
        && !string.IsNullOrEmpty(PhoneNumber);
    private bool CheckEmailValidity()
        => EmailValidityRegex().IsMatch(Email);

    public bool MeetsPasswordPolicy(string password)
    {
        var input = password;

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Password should not be empty");

        Regex? hasNumber = new(@"[0-9]+");
        Regex? hasUpperChar = new(@"[A-Z]+");
        Regex? hasMinChars = new(@".{8,}");
        Regex? hasLowerChar = new(@"[a-z]+");
        Regex? hasSymbols = new(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input)
            || !hasUpperChar.IsMatch(input)
            || !hasMinChars.IsMatch(input)
            || !hasNumber.IsMatch(input)
            || !hasSymbols.IsMatch(input)) return false;

        return true;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailValidityRegex();

}

public partial record CreateCustomerCommand : ICommand<DomainResponse<Guid>>
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Gender { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? CompanyName { get; private set; }
    public string? Description { get; private set; }
    public string? ShippingAddress { get; private set; }
    public Discriminator Discriminator { get; private set; }

    public CreateCustomerCommand(
        string username,
        string email,
        string firstName,
        string lastName,
        string password,
        DateTime dateOfBirth,
        string phoneNumber,
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
        Discriminator = Discriminator.Customer;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Password = password;
        PhoneNumber = phoneNumber;
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
        IsValidPassword = MeetsPasswordPolicy(password);
    }

    public bool IsValidCommand { get; private set; }
    public bool IsValidEmail { get; private set; }
    public bool IsValidPassword { get; private set; }
    private bool CheckValidity()
        => !string.IsNullOrEmpty(Username)
        && !string.IsNullOrEmpty(Email)
        && !string.IsNullOrEmpty(FirstName)
        && !string.IsNullOrEmpty(PhoneNumber);
    private bool CheckEmailValidity()
        => EmailValidityRegex().IsMatch(Email);

    public bool MeetsPasswordPolicy(string password)
    {
        var input = password;

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Password should not be empty");

        Regex? hasNumber = new(@"[0-9]+");
        Regex? hasUpperChar = new(@"[A-Z]+");
        Regex? hasMinChars = new(@".{8,}");
        Regex? hasLowerChar = new(@"[a-z]+");
        Regex? hasSymbols = new(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input)
            || !hasUpperChar.IsMatch(input)
            || !hasMinChars.IsMatch(input)
            || !hasNumber.IsMatch(input)
            || !hasSymbols.IsMatch(input)) return false;

        return true;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailValidityRegex();

}

public partial record CreateAdminCommand : ICommand<DomainResponse<Guid>>
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Gender { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    public string? CompanyName { get; private set; }
    public string? Description { get; private set; }
    public string? ShippingAddress { get; private set; }
    public Discriminator Discriminator { get; private set; }
    public IEnumerable<RoleDto> Roles { get; private set; }

    public CreateAdminCommand(
        string username,
        string email,
        string firstName,
        string lastName,
        string password,
        DateTime dateOfBirth,
        string phoneNumber,
        string gender,
        string street,
        string city,
        string state,
        string country,
        string? description,
        string? companyName,
        string? shippingAddress,
        string postalCode,
        IEnumerable<RoleDto> roles)
    {
        Roles = roles;
        Discriminator = Discriminator.Admin;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Password = password;
        PhoneNumber = phoneNumber;
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
        IsValidPassword = MeetsPasswordPolicy(password);
    }

    public bool IsValidCommand { get; private set; }
    public bool IsValidEmail { get; private set; }
    public bool IsValidPassword { get; private set; }
    private bool CheckValidity()
        => !string.IsNullOrEmpty(Username)
        && !string.IsNullOrEmpty(Email)
        && !string.IsNullOrEmpty(FirstName)
        && !string.IsNullOrEmpty(PhoneNumber);
    private bool CheckEmailValidity()
        => EmailValidityRegex().IsMatch(Email);

    public bool MeetsPasswordPolicy(string password)
    {
        var input = password;

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Password should not be empty");

        Regex? hasNumber = new(@"[0-9]+");
        Regex? hasUpperChar = new(@"[A-Z]+");
        Regex? hasMinChars = new(@".{8,}");
        Regex? hasLowerChar = new(@"[a-z]+");
        Regex? hasSymbols = new(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input)
            || !hasUpperChar.IsMatch(input)
            || !hasMinChars.IsMatch(input)
            || !hasNumber.IsMatch(input)
            || !hasSymbols.IsMatch(input)) return false;

        return true;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailValidityRegex();

}

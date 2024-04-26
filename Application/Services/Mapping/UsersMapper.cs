using Application.Modules.Users.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Mapping;
public static class UserMapper
{
    public static UserDto AsDto(this User user)
        => new (
            user.Id,
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            user.DateOfBirth,
            user.PhoneNumber,
            user.IsActive,
            user.CreatedDate,
            user.LastModifiedDate,
            user.Gender,
            user.Street,
            user.City,
            user.State,
            user.Country,
            user.PostalCode
        );

    public static User AsEntity(this UserDto userDto)
        => new ()
        {
            Id = userDto.id,
            Username = userDto.username,
            Email = userDto.email,
            FirstName = userDto.firstName,
            LastName = userDto.lastName,
            DateOfBirth = userDto.dateOfBirth,
            PhoneNumber = userDto.phoneNumber,
            IsActive = userDto.isActive,
            CreatedDate = userDto.createdDate,
            LastModifiedDate = userDto.lastModifiedDate,
            Gender =  userDto.gender,
            Street = userDto.street,
            City = userDto.city,
            State = userDto.state,
            Country = userDto.country,
            PostalCode = userDto.postalCode
        };
}

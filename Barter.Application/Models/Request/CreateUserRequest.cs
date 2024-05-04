using Barter.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Request;

public class CreateUserRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public long MobileNumber { get; set; }

    public UserRoles Roles { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }
}

using Barter.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Response;

public class UserResponse
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public long MobileNumber { get; set; }

    public UserRoles Roles { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }
}

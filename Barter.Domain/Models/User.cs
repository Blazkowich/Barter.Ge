using Barter.Domain.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace Barter.Domain.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public new string Email { get; set; }

    public string Password { get; set; }

    public long MobileNumber { get; set; }

    public UserRoles Roles { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }
}

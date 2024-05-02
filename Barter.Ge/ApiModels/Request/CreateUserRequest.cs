using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.Api.ApiModels.Request;

public class CreateUserRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public long MobileNumber { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }
}

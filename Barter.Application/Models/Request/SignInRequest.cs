using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Request;

public class SignInRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

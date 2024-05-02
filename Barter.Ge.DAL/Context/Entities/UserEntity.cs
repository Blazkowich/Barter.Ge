namespace Barter.Ge.DAL.Context.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public long MobileNumber { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }
}

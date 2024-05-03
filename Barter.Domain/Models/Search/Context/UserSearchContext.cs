namespace Barter.Domain.Models.Search.Context;

#nullable enable

public class UserSearchContext : SearchContextBase
{
    public Guid? Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public long? MobileNumber { get; set; }

    public string? Address { get; set; }
}

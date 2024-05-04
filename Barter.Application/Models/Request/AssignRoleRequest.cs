using Barter.Domain.Models.Enum;

namespace Barter.Application.Models.Request;

public class AssignRoleRequest
{
    public string UserName { get; set; }
    public UserRoles RoleName { get; set; }
}
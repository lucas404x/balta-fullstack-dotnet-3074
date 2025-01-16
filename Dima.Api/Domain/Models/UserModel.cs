using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Domain.Models;

public class UserModel : IdentityUser<long>
{
    public List<IdentityRoleModel>? Roles { get; set; }
}

public class IdentityRoleModel : IdentityRole<long>
{
    public long UserId { get; set; }
}
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class UserManagementViewModel
{
    public List<IdentityUser> Users { get; set; } = new List<IdentityUser>();
    public IdentityUser NewUser { get; set; } = new IdentityUser();
    public Dictionary<string, string> UserRoles { get; set; } = new Dictionary<string, string>(); // Maps user ID to their role
    public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>(); // List of all roles
}

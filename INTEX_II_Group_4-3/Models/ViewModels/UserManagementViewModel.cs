using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class UserManagementViewModel
{
    public IEnumerable<IdentityUser> Users { get; set; }
    public IdentityUser NewUser { get; set; } = new IdentityUser(); // Use this to bind form data for creating new users
    public Dictionary<string, string> UserRoles { get; set; } = new Dictionary<string, string>(); // Maps user ID to their role
    public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>(); // List of all roles
    public string SelectedRole { get; set; } // Used for assigning a role to a new user
}

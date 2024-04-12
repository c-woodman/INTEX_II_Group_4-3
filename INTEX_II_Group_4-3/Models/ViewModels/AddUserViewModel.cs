using System.ComponentModel.DataAnnotations;

public class AddUserViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string RoleName { get; set; }
}


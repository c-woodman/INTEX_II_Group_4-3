using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace INTEX_II_Group_4_3.Models;

public partial class Customer : IdentityUser
{
    [Key]
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string CountryOfResidence { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public double Age { get; set; }
}

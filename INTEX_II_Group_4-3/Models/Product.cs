using System;
using System.Collections.Generic;

namespace INTEX_II_Group_4_3.Models;

public partial class Product
{
    public double ProductId { get; set; }

    public string Name { get; set; } = null!;

    public double Year { get; set; }

    public double NumParts { get; set; }

    public double Price { get; set; }

    public string? ImgLink { get; set; }

    public string PrimaryColor { get; set; } = null!;

    public string SecondaryColor { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;
}

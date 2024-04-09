using System;
using System.Collections.Generic;

namespace INTEX_II_Group_4_3.Models;

public partial class Order
{
    public double TransactionId { get; set; }

    public double CustomerId { get; set; }

    public DateOnly Date { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public double Time { get; set; }

    public string EntryMode { get; set; } = null!;

    public double Amount { get; set; }

    public string TypeOfTransaction { get; set; } = null!;

    public string CountryOfTransaction { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public string TypeOfCard { get; set; } = null!;

    public double Fraud { get; set; }
}

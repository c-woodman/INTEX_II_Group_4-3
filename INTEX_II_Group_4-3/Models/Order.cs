using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Please enter your country of transaction.")]
    public string CountryOfTransaction { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a shipping address.")]
    public string ShippingAddress { get; set; } = null!;

    [Required(ErrorMessage = "Please enter the bank associated with your card.")]
    public string Bank { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a card type.")]
    public string TypeOfCard { get; set; } = null!;

    public double Fraud { get; set; }
}

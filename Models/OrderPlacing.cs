using System;
using System.Collections.Generic;

namespace PizzaApplication.Models;

public partial class OrderPlacing
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string ProductName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int? Quantity { get; set; }

    public double? TotalAmount { get; set; }
}

using System;
using System.Collections.Generic;

namespace PizzaApplication.Models;

public partial class OrderPlaced
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int? Quantity { get; set; }

    public double? TotalAmount { get; set; }
}

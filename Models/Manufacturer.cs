using System;
using System.Collections.Generic;

namespace Avtoservice.Models;

public partial class Manufacturer
{
    public int ManId { get; set; }

    public string? ManName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

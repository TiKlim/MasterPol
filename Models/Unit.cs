using System;
using System.Collections.Generic;

namespace Avtoservice.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

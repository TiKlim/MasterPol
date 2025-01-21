using System;
using System.Collections.Generic;

namespace Avtoservice.Models;

public partial class Provider
{
    public int ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

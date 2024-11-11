using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class ProductType
{
    public int Id { get; set; }

    public string? ProductTypeName { get; set; }

    public double? Ratio { get; set; }
}

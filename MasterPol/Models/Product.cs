using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? ProductType { get; set; }

    public string? ProductName { get; set; }

    public string? ArticleNumber { get; set; }

    public double? MinimumCost { get; set; }

    public virtual ICollection<PartnerProductsImport> PartnerProductsImports { get; set; } = new List<PartnerProductsImport>();
}

using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class PartnerProductsImport
{
    public int Id { get; set; }

    public int? Products { get; set; }

    public int? PartnerName { get; set; }

    public string? ProductsCount { get; set; }

    public DateOnly? DateOfRealization { get; set; }

    public virtual Partner? PartnerNameNavigation { get; set; }

    public virtual Product? ProductsNavigation { get; set; }
}

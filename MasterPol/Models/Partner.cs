using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Partner
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string? Name { get; set; }

    public string? Director { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Inn { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<PartnerProductsImport> PartnerProductsImports { get; set; } = new List<PartnerProductsImport>();

    public virtual Rating? RatingNavigation { get; set; }

    public virtual Type? Type { get; set; }
}

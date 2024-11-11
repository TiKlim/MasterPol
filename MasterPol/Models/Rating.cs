using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Rating
{
    public int Id { get; set; }

    public string? RatingType { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}

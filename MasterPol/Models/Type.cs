using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Type
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}

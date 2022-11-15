using System;
using System.Collections.Generic;

namespace nummerplade_api.Model;

public partial class Policecar
{
    public long Id { get; set; }

    public string Nrplade { get; set; } = null!;

    public DateTime AddedDate { get; set; }
}

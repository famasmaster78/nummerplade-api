using System;
using System.Collections.Generic;

namespace nummerplade_api.Model;

public partial class Spotting
{
    public long Id { get; set; }

    public string? Nrplade { get; set; }

    public DateTime AddedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public DateTime ValidUntilDate { get; set; }

    public double? LocationX { get; set; }

    public double? LocationY { get; set; }
}

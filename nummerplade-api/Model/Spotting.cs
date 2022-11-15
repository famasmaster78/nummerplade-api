﻿using System;
using System.Collections.Generic;

namespace nummerplade_api.Model;

public partial class Spotting
{
    public long Id { get; set; }

    public string Nrplade { get; set; } = null!;

    public DateTime AddedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public DateTime ValidUntilDate { get; set; }

    public string Location { get; set; } = null!;
}
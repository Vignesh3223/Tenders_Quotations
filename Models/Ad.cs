using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class Ad
{
    public int AdId { get; set; }

    public string? AdTitle { get; set; }

    public string? AdPoster { get; set; }
}

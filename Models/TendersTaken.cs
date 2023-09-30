using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class TendersTaken
{
    public int TakenId { get; set; }

    public int? QuotationId { get; set; }

    public string? TenderName { get; set; }

    public string? CompanyName { get; set; }

    public string? Proprieator { get; set; }

    public int? QuoteAmount { get; set; }

    public string? Location { get; set; }

    public string? Authority { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public virtual Quotation? Quotation { get; set; }
}

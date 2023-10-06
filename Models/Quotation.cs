using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class Quotation
{
    public int QuotationId { get; set; }

    public int? UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? Proprieator { get; set; }

    public int? TenderId { get; set; }

    public string? TenderName { get; set; }

    public string? Location { get; set; }

    public string? Authority { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public DateTime? QuotedDate { get; set; }

    public int? QuoteAmount { get; set; }

    public string? EstablishedDate { get; set; }

    public int? ProjectValue { get; set; }

    public string? Email { get; set; }

    public virtual Tender? Tender { get; set; }

    public virtual ICollection<TendersTaken> TendersTakens { get; set; } = new List<TendersTaken>();

    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class Tender
{
    public int TenderId { get; set; }

    public string? TenderName { get; set; }

    public string? Referencenumber { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? ProjectValue { get; set; }

    public DateTime? TenderOpeningDate { get; set; }

    public DateTime? TenderClosingDate { get; set; }

    public string? Location { get; set; }

    public string? Authority { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public int? ApplicationFee { get; set; }

    public bool? IsTaken { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();

    public virtual ICollection<TendersTaken> TendersTakens { get; set; } = new List<TendersTaken>();
}

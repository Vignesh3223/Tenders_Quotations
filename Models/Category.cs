using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Tender> Tenders { get; set; } = new List<Tender>();
}

using System;
using System.Collections.Generic;

namespace Tenders_Quotations.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? Proprieator { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? ContactNumber { get; set; }

    public string? CompanySector { get; set; }

    public string? EstablishedDate { get; set; }

    public string? Gstin { get; set; }

    public int? Crn { get; set; }

    public string? Password { get; set; }

    public byte[]? ProfilePic { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();

    public virtual Role? Role { get; set; }
}

namespace Tenders_Quotations.Models
{
    public class EmailResponse
    {
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? TenderName { get; set; }
        public string? Location { get; set; }
        public string? Authority { get; set;}
        public int? ProjectValue { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set;}

    }
}

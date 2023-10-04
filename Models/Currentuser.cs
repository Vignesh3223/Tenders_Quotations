using Microsoft.EntityFrameworkCore;

namespace Tenders_Quotations.Models
{
    [Keyless]
    public class Currentuser
    {
        public int UserId { get; set; }
        public string? Roles { get; set; }
    }
}

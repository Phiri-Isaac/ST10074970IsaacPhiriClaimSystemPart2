namespace ClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Status { get; set; } = "Pending";
        public string? SupportingDocumentPath { get; set; } // optional
    }
}

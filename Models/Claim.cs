namespace ClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }

        public string Status { get; set; } = "Pending";

        public string? SupportingDocumentPath { get; set; }
        public string? Notes { get; set; }

        // computed total (not stored)
        public decimal TotalAmount => HoursWorked * HourlyRate;
    }
}
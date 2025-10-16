namespace ClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;

        // ✅ use decimal for HoursWorked for proper numeric binding
        public decimal HoursWorked { get; set; }

        public decimal HourlyRate { get; set; }

        public string Status { get; set; } = "Pending";

        public string? SupportingDocumentPath { get; set; }

        public string? Notes { get; set; }

        // ✅ Computed property for display
        public decimal TotalAmount => HoursWorked * HourlyRate;
    }
}
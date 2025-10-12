namespace ClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public string ClaimDescription { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
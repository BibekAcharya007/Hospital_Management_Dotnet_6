namespace Hospital_Management.Api.Models.Billing
{
    public class InsuranceClaim
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string? ProviderName { get; set; }
        public string? Status { get; set; } // Submitted, Approved, Rejected

        // Navigation properties
        public Bill? Bill { get; set; }
    }
}

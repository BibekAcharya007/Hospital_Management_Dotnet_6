namespace Hospital_Management.Api.DTOs.Billing
{
    public class InsuranceClaimDto
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string? ProviderName { get; set; }
        public string? Status { get; set; }
    }

    public class CreateInsuranceClaimDto
    {
        public int BillId { get; set; }
        public string? ProviderName { get; set; }
        public string? Status { get; set; }
    }

    public class UpdateInsuranceClaimDto
    {
        public string? ProviderName { get; set; }
        public string? Status { get; set; }
    }
}

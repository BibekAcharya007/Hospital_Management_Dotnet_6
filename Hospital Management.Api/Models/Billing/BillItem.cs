namespace HospitalManagement.Api.Models.Billing
{
    public class BillItem
    {
        public int Id { get; set; }
        public int BillId { get; set; }

        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }
}

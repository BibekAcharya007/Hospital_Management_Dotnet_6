namespace Hospital_Management.Api.DTOs.Billing
{
    public class BillItemDto
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreateBillItemDto
    {
        public int BillId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateBillItemDto
    {
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }
}

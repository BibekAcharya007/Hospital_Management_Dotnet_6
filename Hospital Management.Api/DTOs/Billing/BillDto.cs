namespace Hospital_Management.Api.DTOs.Billing
{
    public class BillDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public string? PatientName { get; set; }
    }

    public class CreateBillDto
    {
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }

    public class UpdateBillDto
    {
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}

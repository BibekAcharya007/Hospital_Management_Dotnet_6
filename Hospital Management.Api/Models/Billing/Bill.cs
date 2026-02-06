namespace Hospital_Management.Api.Models.Billing
{
    public class Bill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}

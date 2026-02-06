using Hospital_Management.Api.Models.Patients;

namespace Hospital_Management.Api.Models.Billing
{
    public class Bill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
        public InsuranceClaim? InsuranceClaim { get; set; }
    }
}

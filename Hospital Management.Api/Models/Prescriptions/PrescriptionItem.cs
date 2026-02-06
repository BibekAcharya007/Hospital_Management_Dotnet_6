namespace Hospital_Management.Api.Models.Prescriptions
{
    public class PrescriptionItem
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string? Dosage { get; set; }
        public int DurationDays { get; set; }

        // Navigation properties
        public Prescription? Prescription { get; set; }
        public Medicine? Medicine { get; set; }
    }
}

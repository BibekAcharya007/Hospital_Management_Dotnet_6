namespace Hospital_Management.Api.DTOs.Prescriptions
{
    public class PrescriptionItemDto
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string? Dosage { get; set; }
        public int DurationDays { get; set; }
        public string? MedicineName { get; set; }
    }

    public class CreatePrescriptionItemDto
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string? Dosage { get; set; }
        public int DurationDays { get; set; }
    }

    public class UpdatePrescriptionItemDto
    {
        public int MedicineId { get; set; }
        public string? Dosage { get; set; }
        public int DurationDays { get; set; }
    }
}

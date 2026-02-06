namespace Hospital_Management.Api.DTOs.Patients
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMedicalRecordDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateMedicalRecordDto
    {
        public string? Notes { get; set; }
    }
}

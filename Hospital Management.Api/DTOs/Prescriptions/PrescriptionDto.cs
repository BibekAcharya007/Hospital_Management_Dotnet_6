namespace Hospital_Management.Api.DTOs.Prescriptions
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateIssued { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
    }

    public class CreatePrescriptionDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateIssued { get; set; }
    }

    public class UpdatePrescriptionDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateIssued { get; set; }
    }
}

namespace Hospital_Management.Api.DTOs.Admissions
{
    public class DischargeDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public string? DischargeNotes { get; set; }
        public string? PatientName { get; set; }
    }

    public class CreateDischargeDto
    {
        public int PatientId { get; set; }
        public int AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public string? DischargeNotes { get; set; }
    }

    public class UpdateDischargeDto
    {
        public DateTime DischargeDate { get; set; }
        public string? DischargeNotes { get; set; }
    }
}

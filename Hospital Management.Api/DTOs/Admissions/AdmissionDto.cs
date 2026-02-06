namespace Hospital_Management.Api.DTOs.Admissions
{
    public class AdmissionDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string? PatientName { get; set; }
    }

    public class CreateAdmissionDto
    {
        public int PatientId { get; set; }
        public DateTime AdmissionDate { get; set; }
    }

    public class UpdateAdmissionDto
    {
        public DateTime AdmissionDate { get; set; }
    }
}

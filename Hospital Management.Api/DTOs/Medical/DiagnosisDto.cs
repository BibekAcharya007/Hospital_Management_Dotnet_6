namespace Hospital_Management.Api.DTOs.Medical
{
    public class DiagnosisDto
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public string? Description { get; set; }
    }

    public class CreateDiagnosisDto
    {
        public int MedicalRecordId { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDiagnosisDto
    {
        public string? Description { get; set; }
    }
}

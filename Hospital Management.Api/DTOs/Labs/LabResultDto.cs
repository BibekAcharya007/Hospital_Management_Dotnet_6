namespace Hospital_Management.Api.DTOs.Labs
{
    public class LabResultDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int LabTestId { get; set; }
        public string? Result { get; set; }
        public DateTime TestDate { get; set; }
        public string? PatientName { get; set; }
        public string? LabTestName { get; set; }
    }

    public class CreateLabResultDto
    {
        public int PatientId { get; set; }
        public int LabTestId { get; set; }
        public string? Result { get; set; }
        public DateTime TestDate { get; set; }
    }

    public class UpdateLabResultDto
    {
        public string? Result { get; set; }
        public DateTime TestDate { get; set; }
    }
}

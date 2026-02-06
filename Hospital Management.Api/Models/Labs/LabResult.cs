namespace Hospital_Management.Api.Models.Labs
{
    public class LabResult
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int LabTestId { get; set; }

        public string? Result { get; set; }
        public DateTime TestDate { get; set; }
    }
}

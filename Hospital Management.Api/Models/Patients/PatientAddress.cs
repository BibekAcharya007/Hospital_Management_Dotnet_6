namespace Hospital_Management.Api.Models.Patients
{
    public class PatientAddress
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
    }
}

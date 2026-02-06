namespace Hospital_Management.Api.DTOs.Patients
{
    public class PatientAddressDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }

    public class CreatePatientAddressDto
    {
        public int PatientId { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }

    public class UpdatePatientAddressDto
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}

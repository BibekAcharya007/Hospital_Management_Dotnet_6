namespace Hospital_Management.Api.DTOs.Patients
{
    public class UpdatePatientDto
    {
        public string? FullName { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicConditions { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }
}

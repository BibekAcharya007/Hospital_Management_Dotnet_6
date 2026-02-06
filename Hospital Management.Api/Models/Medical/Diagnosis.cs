using Hospital_Management.Api.Models.Patients;

namespace Hospital_Management.Api.Models.Medical
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public MedicalRecord? MedicalRecord { get; set; }
    }
}

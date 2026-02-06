using Hospital_Management.Api.Models.Patients;

namespace Hospital_Management.Api.Models.Admissions
{
    public class Admission
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AdmissionDate { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Discharge? Discharge { get; set; }
    }
}

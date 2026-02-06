using Hospital_Management.Api.Models.Patients;

namespace Hospital_Management.Api.Models.Admissions
{
    public class Discharge
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public string? DischargeNotes { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Admission? Admission { get; set; }
    }
}

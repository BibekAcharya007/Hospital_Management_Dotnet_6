using Hospital_Management.Api.Models.Admissions;
using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Billing;
using Hospital_Management.Api.Models.Labs;
using Hospital_Management.Api.Models.Prescriptions;

namespace Hospital_Management.Api.Models.Patients
{
    public class Patient
    {
        public int Id { get; set; }
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
        public DateTime EntryTime { get; set; }

        // Navigation properties
        public ICollection<PatientAddress> Addresses { get; set; } = new List<PatientAddress>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
        public ICollection<Discharge> Discharges { get; set; } = new List<Discharge>();
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
    }
}

using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Prescriptions;

namespace Hospital_Management.Api.Models.Doctors
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int DepartmentId { get; set; }
        public int SpecializationId { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public DoctorSpecialization? Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}

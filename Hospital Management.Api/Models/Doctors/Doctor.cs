namespace Hospital_Management.Api.Models.Doctors
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? FullName { get; set; }

        public int DepartmentId { get; set; }
        public int SpecializationId { get; set; }

        public Department? Department { get; set; }
        public DoctorSpecialization? Specialization { get; set; }
    }
}

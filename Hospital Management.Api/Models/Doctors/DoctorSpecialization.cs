namespace Hospital_Management.Api.Models.Doctors
{
    public class DoctorSpecialization
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation properties
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}

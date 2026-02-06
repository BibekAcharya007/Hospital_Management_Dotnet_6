namespace Hospital_Management.Api.Models.Appointments
{
    public class AppointmentStatus
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

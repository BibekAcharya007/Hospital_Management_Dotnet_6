namespace Hospital_Management.Api.DTOs.Appointments
{
    public class AppointmentStatusDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class CreateAppointmentStatusDto
    {
        public string? Name { get; set; }
    }

    public class UpdateAppointmentStatusDto
    {
        public string? Name { get; set; }
    }
}

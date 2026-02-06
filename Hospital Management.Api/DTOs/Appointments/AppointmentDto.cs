namespace Hospital_Management.Api.DTOs.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? StatusName { get; set; }
    }

    public class CreateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusId { get; set; }
    }

    public class UpdateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusId { get; set; }
    }
}

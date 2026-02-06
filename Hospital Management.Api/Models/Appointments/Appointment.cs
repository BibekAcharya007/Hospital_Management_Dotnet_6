namespace Hospital_Management.Api.Models.Appointments
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int StatusId { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public AppointmentStatus? Status { get; set; }
    }
}

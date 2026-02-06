namespace Hospital_Management.Api.DTOs.Doctors
{
    public class DoctorSpecializationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class CreateDoctorSpecializationDto
    {
        public string? Name { get; set; }
    }

    public class UpdateDoctorSpecializationDto
    {
        public string? Name { get; set; }
    }
}

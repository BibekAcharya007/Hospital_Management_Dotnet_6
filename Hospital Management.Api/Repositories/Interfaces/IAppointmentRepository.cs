using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Admissions;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        // Appointments
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task AddAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);

        // Appointment Statuses
        Task<IEnumerable<AppointmentStatus>> GetAllStatusesAsync();
        Task<AppointmentStatus?> GetStatusByIdAsync(int id);
        Task AddStatusAsync(AppointmentStatus status);
        Task<bool> UpdateStatusAsync(AppointmentStatus status);
        Task<bool> DeleteStatusAsync(int id);

        // Admissions
        Task<IEnumerable<Admission>> GetAllAdmissionsAsync();
        Task<Admission?> GetAdmissionByIdAsync(int id);
        Task AddAdmissionAsync(Admission admission);
        Task<bool> UpdateAdmissionAsync(Admission admission);
        Task<bool> DeleteAdmissionAsync(int id);

        // Discharges
        Task<IEnumerable<Discharge>> GetAllDischargesAsync();
        Task<Discharge?> GetDischargeByIdAsync(int id);
        Task AddDischargeAsync(Discharge discharge);
        Task<bool> UpdateDischargeAsync(Discharge discharge);
        Task<bool> DeleteDischargeAsync(int id);

        Task SaveChangesAsync();
    }
}

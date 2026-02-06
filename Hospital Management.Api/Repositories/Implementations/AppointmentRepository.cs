using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Admissions;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── APPOINTMENTS ─────────────

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
            => await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Status)
                .ToListAsync();

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
            => await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── APPOINTMENT STATUSES ─────────────

        public async Task<IEnumerable<AppointmentStatus>> GetAllStatusesAsync()
            => await _context.AppointmentStatuses.ToListAsync();

        public async Task<AppointmentStatus?> GetStatusByIdAsync(int id)
            => await _context.AppointmentStatuses.FindAsync(id);

        public async Task AddStatusAsync(AppointmentStatus status)
        {
            _context.AppointmentStatuses.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatusAsync(AppointmentStatus status)
        {
            _context.AppointmentStatuses.Update(status);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStatusAsync(int id)
        {
            var status = await _context.AppointmentStatuses.FindAsync(id);
            if (status == null) return false;
            _context.AppointmentStatuses.Remove(status);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── ADMISSIONS ─────────────

        public async Task<IEnumerable<Admission>> GetAllAdmissionsAsync()
            => await _context.Admissions
                .Include(a => a.Patient)
                .ToListAsync();

        public async Task<Admission?> GetAdmissionByIdAsync(int id)
            => await _context.Admissions
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAdmissionAsync(Admission admission)
        {
            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAdmissionAsync(Admission admission)
        {
            _context.Admissions.Update(admission);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAdmissionAsync(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null) return false;
            _context.Admissions.Remove(admission);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── DISCHARGES ─────────────

        public async Task<IEnumerable<Discharge>> GetAllDischargesAsync()
            => await _context.Discharges
                .Include(d => d.Patient)
                .ToListAsync();

        public async Task<Discharge?> GetDischargeByIdAsync(int id)
            => await _context.Discharges
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task AddDischargeAsync(Discharge discharge)
        {
            _context.Discharges.Add(discharge);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDischargeAsync(Discharge discharge)
        {
            _context.Discharges.Update(discharge);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDischargeAsync(int id)
        {
            var discharge = await _context.Discharges.FindAsync(id);
            if (discharge == null) return false;
            _context.Discharges.Remove(discharge);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}

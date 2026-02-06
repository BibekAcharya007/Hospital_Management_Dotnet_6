using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Doctors;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── DOCTORS ─────────────

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
            => await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .ToListAsync();

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
            => await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── DEPARTMENTS ─────────────

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
            => await _context.Departments.ToListAsync();

        public async Task<Department?> GetDepartmentByIdAsync(int id)
            => await _context.Departments.FindAsync(id);

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return false;
            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── SPECIALIZATIONS ─────────────

        public async Task<IEnumerable<DoctorSpecialization>> GetAllSpecializationsAsync()
            => await _context.DoctorSpecializations.ToListAsync();

        public async Task<DoctorSpecialization?> GetSpecializationByIdAsync(int id)
            => await _context.DoctorSpecializations.FindAsync(id);

        public async Task AddSpecializationAsync(DoctorSpecialization specialization)
        {
            _context.DoctorSpecializations.Add(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSpecializationAsync(DoctorSpecialization specialization)
        {
            _context.DoctorSpecializations.Update(specialization);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSpecializationAsync(int id)
        {
            var spec = await _context.DoctorSpecializations.FindAsync(id);
            if (spec == null) return false;
            _context.DoctorSpecializations.Remove(spec);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}

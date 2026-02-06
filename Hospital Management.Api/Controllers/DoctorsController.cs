using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.DTOs.Doctors;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Doctors;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DoctorsController(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── DOCTORS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .Select(d => new DoctorDto
                {
                    Id = d.Id, FullName = d.FullName,
                    DepartmentId = d.DepartmentId, SpecializationId = d.SpecializationId,
                    DepartmentName = d.Department!.Name, SpecializationName = d.Specialization!.Name
                }).ToListAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Department)
                .Include(d => d.Specialization)
                .Where(d => d.Id == id)
                .Select(d => new DoctorDto
                {
                    Id = d.Id, FullName = d.FullName,
                    DepartmentId = d.DepartmentId, SpecializationId = d.SpecializationId,
                    DepartmentName = d.Department!.Name, SpecializationName = d.Specialization!.Name
                }).FirstOrDefaultAsync();

            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FullName = dto.FullName, DepartmentId = dto.DepartmentId,
                SpecializationId = dto.SpecializationId
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            doctor.FullName = dto.FullName;
            doctor.DepartmentId = dto.DepartmentId;
            doctor.SpecializationId = dto.SpecializationId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── DEPARTMENTS ─────────────

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _context.Departments
                .Select(d => new DepartmentDto { Id = d.Id, Name = d.Name })
                .ToListAsync();

            return Ok(departments);
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var dept = await _context.Departments
                .Where(d => d.Id == id)
                .Select(d => new DepartmentDto { Id = d.Id, Name = d.Name })
                .FirstOrDefaultAsync();

            if (dept == null) return NotFound();
            return Ok(dept);
        }

        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            var dept = new Department { Name = dto.Name };
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return Created($"api/doctors/departments/{dept.Id}", new DepartmentDto { Id = dept.Id, Name = dept.Name });
        }

        [HttpPut("departments/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto dto)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            dept.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── SPECIALIZATIONS ─────────────

        [HttpGet("specializations")]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var specs = await _context.DoctorSpecializations
                .Select(s => new DoctorSpecializationDto { Id = s.Id, Name = s.Name })
                .ToListAsync();

            return Ok(specs);
        }

        [HttpGet("specializations/{id}")]
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            var spec = await _context.DoctorSpecializations
                .Where(s => s.Id == id)
                .Select(s => new DoctorSpecializationDto { Id = s.Id, Name = s.Name })
                .FirstOrDefaultAsync();

            if (spec == null) return NotFound();
            return Ok(spec);
        }

        [HttpPost("specializations")]
        public async Task<IActionResult> CreateSpecialization(CreateDoctorSpecializationDto dto)
        {
            var spec = new DoctorSpecialization { Name = dto.Name };
            _context.DoctorSpecializations.Add(spec);
            await _context.SaveChangesAsync();
            return Created($"api/doctors/specializations/{spec.Id}", new DoctorSpecializationDto { Id = spec.Id, Name = spec.Name });
        }

        [HttpPut("specializations/{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, UpdateDoctorSpecializationDto dto)
        {
            var spec = await _context.DoctorSpecializations.FindAsync(id);
            if (spec == null) return NotFound();

            spec.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("specializations/{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var spec = await _context.DoctorSpecializations.FindAsync(id);
            if (spec == null) return NotFound();

            _context.DoctorSpecializations.Remove(spec);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

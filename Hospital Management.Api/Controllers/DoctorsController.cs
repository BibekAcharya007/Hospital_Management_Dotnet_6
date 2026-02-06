using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Api.DTOs.Doctors;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Doctors;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    [Authorize(Roles = "Admin,Doctor")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _repo;

        public DoctorsController(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // ───────────── DOCTORS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = (await _repo.GetAllDoctorsAsync())
                .Select(d => new DoctorDto
                {
                    Id = d.Id, FullName = d.FullName,
                    DepartmentId = d.DepartmentId, SpecializationId = d.SpecializationId,
                    DepartmentName = d.Department!.Name, SpecializationName = d.Specialization!.Name
                }).ToList();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var d = await _repo.GetDoctorByIdAsync(id);
            if (d == null) return NotFound();

            return Ok(new DoctorDto
            {
                Id = d.Id, FullName = d.FullName,
                DepartmentId = d.DepartmentId, SpecializationId = d.SpecializationId,
                DepartmentName = d.Department!.Name, SpecializationName = d.Specialization!.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FullName = dto.FullName, DepartmentId = dto.DepartmentId,
                SpecializationId = dto.SpecializationId
            };

            await _repo.AddDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorDto dto)
        {
            var doctor = await _repo.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            doctor.FullName = dto.FullName;
            doctor.DepartmentId = dto.DepartmentId;
            doctor.SpecializationId = dto.SpecializationId;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var deleted = await _repo.DeleteDoctorAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── DEPARTMENTS ─────────────

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = (await _repo.GetAllDepartmentsAsync())
                .Select(d => new DepartmentDto { Id = d.Id, Name = d.Name })
                .ToList();

            return Ok(departments);
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var dept = await _repo.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return Ok(new DepartmentDto { Id = dept.Id, Name = dept.Name });
        }

        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            var dept = new Department { Name = dto.Name };
            await _repo.AddDepartmentAsync(dept);
            return Created($"api/doctors/departments/{dept.Id}", new DepartmentDto { Id = dept.Id, Name = dept.Name });
        }

        [HttpPut("departments/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto dto)
        {
            var dept = await _repo.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();

            dept.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _repo.DeleteDepartmentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── SPECIALIZATIONS ─────────────

        [HttpGet("specializations")]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var specs = (await _repo.GetAllSpecializationsAsync())
                .Select(s => new DoctorSpecializationDto { Id = s.Id, Name = s.Name })
                .ToList();

            return Ok(specs);
        }

        [HttpGet("specializations/{id}")]
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            var spec = await _repo.GetSpecializationByIdAsync(id);
            if (spec == null) return NotFound();
            return Ok(new DoctorSpecializationDto { Id = spec.Id, Name = spec.Name });
        }

        [HttpPost("specializations")]
        public async Task<IActionResult> CreateSpecialization(CreateDoctorSpecializationDto dto)
        {
            var spec = new DoctorSpecialization { Name = dto.Name };
            await _repo.AddSpecializationAsync(spec);
            return Created($"api/doctors/specializations/{spec.Id}", new DoctorSpecializationDto { Id = spec.Id, Name = spec.Name });
        }

        [HttpPut("specializations/{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, UpdateDoctorSpecializationDto dto)
        {
            var spec = await _repo.GetSpecializationByIdAsync(id);
            if (spec == null) return NotFound();

            spec.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("specializations/{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var deleted = await _repo.DeleteSpecializationAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

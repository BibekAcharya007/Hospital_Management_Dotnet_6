using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Api.DTOs.Appointments;
using Hospital_Management.Api.DTOs.Admissions;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Admissions;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]//enables api specific behaviour
    [Route("api/[controller]")]//defines the url
    [ValidateModel]//Automatically checks if the incoming request data (DTO) is valid
    [ApiResponseWrapper]//result filter 
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentsController(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        // ───────────── APPOINTMENTS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = (await _repo.GetAllAppointmentsAsync())
                .Select(a => new AppointmentDto
                {
                    Id = a.Id, PatientId = a.PatientId, DoctorId = a.DoctorId,
                    AppointmentDate = a.AppointmentDate, StatusId = a.StatusId,
                    PatientName = a.Patient!.FullName, DoctorName = a.Doctor!.FullName,
                    StatusName = a.Status!.Name
                }).ToList();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var a = await _repo.GetAppointmentByIdAsync(id);
            if (a == null) return NotFound();

            return Ok(new AppointmentDto
            {
                Id = a.Id, PatientId = a.PatientId, DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate, StatusId = a.StatusId,
                PatientName = a.Patient!.FullName, DoctorName = a.Doctor!.FullName,
                StatusName = a.Status!.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId, DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate, StatusId = dto.StatusId
            };

            await _repo.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, UpdateAppointmentDto dto)
        {
            var appointment = await _repo.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();

            appointment.PatientId = dto.PatientId; appointment.DoctorId = dto.DoctorId;
            appointment.AppointmentDate = dto.AppointmentDate; appointment.StatusId = dto.StatusId;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var deleted = await _repo.DeleteAppointmentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── APPOINTMENT STATUSES ─────────────

        [HttpGet("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = (await _repo.GetAllStatusesAsync())
                .Select(s => new AppointmentStatusDto { Id = s.Id, Name = s.Name })
                .ToList();

            return Ok(statuses);
        }

        [HttpPost("statuses")]
        public async Task<IActionResult> CreateStatus(CreateAppointmentStatusDto dto)
        {
            var status = new AppointmentStatus { Name = dto.Name };
            await _repo.AddStatusAsync(status);
            return Created($"api/appointments/statuses", new AppointmentStatusDto { Id = status.Id, Name = status.Name });
        }

        [HttpPut("statuses/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateAppointmentStatusDto dto)
        {
            var status = await _repo.GetStatusByIdAsync(id);
            if (status == null) return NotFound();

            status.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("statuses/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var deleted = await _repo.DeleteStatusAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── ADMISSIONS ─────────────

        [HttpGet("admissions")]
        public async Task<IActionResult> GetAllAdmissions()
        {
            var admissions = (await _repo.GetAllAdmissionsAsync())
                .Select(a => new AdmissionDto
                {
                    Id = a.Id, PatientId = a.PatientId,
                    AdmissionDate = a.AdmissionDate, PatientName = a.Patient!.FullName
                }).ToList();

            return Ok(admissions);
        }

        [HttpGet("admissions/{id}")]
        public async Task<IActionResult> GetAdmissionById(int id)
        {
            var a = await _repo.GetAdmissionByIdAsync(id);
            if (a == null) return NotFound();

            return Ok(new AdmissionDto
            {
                Id = a.Id, PatientId = a.PatientId,
                AdmissionDate = a.AdmissionDate, PatientName = a.Patient!.FullName
            });
        }

        [HttpPost("admissions")]
        public async Task<IActionResult> CreateAdmission(CreateAdmissionDto dto)
        {
            var admission = new Admission
            {
                PatientId = dto.PatientId, AdmissionDate = dto.AdmissionDate
            };

            await _repo.AddAdmissionAsync(admission);
            return Created($"api/appointments/admissions/{admission.Id}", dto);
        }

        [HttpPut("admissions/{id}")]
        public async Task<IActionResult> UpdateAdmission(int id, UpdateAdmissionDto dto)
        {
            var admission = await _repo.GetAdmissionByIdAsync(id);
            if (admission == null) return NotFound();

            admission.AdmissionDate = dto.AdmissionDate;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("admissions/{id}")]
        public async Task<IActionResult> DeleteAdmission(int id)
        {
            var deleted = await _repo.DeleteAdmissionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── DISCHARGES ─────────────

        [HttpGet("discharges")]
        public async Task<IActionResult> GetAllDischarges()
        {
            var discharges = (await _repo.GetAllDischargesAsync())
                .Select(d => new DischargeDto
                {
                    Id = d.Id, PatientId = d.PatientId, AdmissionId = d.AdmissionId,
                    DischargeDate = d.DischargeDate, DischargeNotes = d.DischargeNotes,
                    PatientName = d.Patient!.FullName
                }).ToList();

            return Ok(discharges);
        }

        [HttpGet("discharges/{id}")]
        public async Task<IActionResult> GetDischargeById(int id)
        {
            var d = await _repo.GetDischargeByIdAsync(id);
            if (d == null) return NotFound();

            return Ok(new DischargeDto
            {
                Id = d.Id, PatientId = d.PatientId, AdmissionId = d.AdmissionId,
                DischargeDate = d.DischargeDate, DischargeNotes = d.DischargeNotes,
                PatientName = d.Patient!.FullName
            });
        }

        [HttpPost("discharges")]
        public async Task<IActionResult> CreateDischarge(CreateDischargeDto dto)
        {
            var discharge = new Discharge
            {
                PatientId = dto.PatientId, AdmissionId = dto.AdmissionId,
                DischargeDate = dto.DischargeDate, DischargeNotes = dto.DischargeNotes
            };

            await _repo.AddDischargeAsync(discharge);
            return Created($"api/appointments/discharges/{discharge.Id}", dto);
        }

        [HttpPut("discharges/{id}")]
        public async Task<IActionResult> UpdateDischarge(int id, UpdateDischargeDto dto)
        {
            var discharge = await _repo.GetDischargeByIdAsync(id);
            if (discharge == null) return NotFound();

            discharge.DischargeDate = dto.DischargeDate;
            discharge.DischargeNotes = dto.DischargeNotes;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("discharges/{id}")]
        public async Task<IActionResult> DeleteDischarge(int id)
        {
            var deleted = await _repo.DeleteDischargeAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

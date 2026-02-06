using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.DTOs.Appointments;
using Hospital_Management.Api.DTOs.Admissions;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Admissions;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    public class AppointmentsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public AppointmentsController(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── APPOINTMENTS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Select(a => new AppointmentDto
                {
                    Id = a.Id, PatientId = a.PatientId, DoctorId = a.DoctorId,
                    AppointmentDate = a.AppointmentDate, StatusId = a.StatusId,
                    PatientName = a.Patient!.FullName, DoctorName = a.Doctor!.FullName,
                    StatusName = a.Status!.Name
                }).ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _context.Appointments
                .Where(a => a.Id == id)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id, PatientId = a.PatientId, DoctorId = a.DoctorId,
                    AppointmentDate = a.AppointmentDate, StatusId = a.StatusId,
                    PatientName = a.Patient!.FullName, DoctorName = a.Doctor!.FullName,
                    StatusName = a.Status!.Name
                }).FirstOrDefaultAsync();

            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId, DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate, StatusId = dto.StatusId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, UpdateAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.PatientId = dto.PatientId; appointment.DoctorId = dto.DoctorId;
            appointment.AppointmentDate = dto.AppointmentDate; appointment.StatusId = dto.StatusId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── APPOINTMENT STATUSES ─────────────

        [HttpGet("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _context.AppointmentStatuses
                .Select(s => new AppointmentStatusDto { Id = s.Id, Name = s.Name })
                .ToListAsync();

            return Ok(statuses);
        }

        [HttpPost("statuses")]
        public async Task<IActionResult> CreateStatus(CreateAppointmentStatusDto dto)
        {
            var status = new AppointmentStatus { Name = dto.Name };
            _context.AppointmentStatuses.Add(status);
            await _context.SaveChangesAsync();
            return Created($"api/appointments/statuses", new AppointmentStatusDto { Id = status.Id, Name = status.Name });
        }

        [HttpPut("statuses/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateAppointmentStatusDto dto)
        {
            var status = await _context.AppointmentStatuses.FindAsync(id);
            if (status == null) return NotFound();

            status.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("statuses/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.AppointmentStatuses.FindAsync(id);
            if (status == null) return NotFound();

            _context.AppointmentStatuses.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── ADMISSIONS ─────────────

        [HttpGet("admissions")]
        public async Task<IActionResult> GetAllAdmissions()
        {
            var admissions = await _context.Admissions
                .Select(a => new AdmissionDto
                {
                    Id = a.Id, PatientId = a.PatientId,
                    AdmissionDate = a.AdmissionDate, PatientName = a.Patient!.FullName
                }).ToListAsync();

            return Ok(admissions);
        }

        [HttpGet("admissions/{id}")]
        public async Task<IActionResult> GetAdmissionById(int id)
        {
            var admission = await _context.Admissions
                .Where(a => a.Id == id)
                .Select(a => new AdmissionDto
                {
                    Id = a.Id, PatientId = a.PatientId,
                    AdmissionDate = a.AdmissionDate, PatientName = a.Patient!.FullName
                }).FirstOrDefaultAsync();

            if (admission == null) return NotFound();
            return Ok(admission);
        }

        [HttpPost("admissions")]
        public async Task<IActionResult> CreateAdmission(CreateAdmissionDto dto)
        {
            var admission = new Admission
            {
                PatientId = dto.PatientId, AdmissionDate = dto.AdmissionDate
            };

            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();
            return Created($"api/appointments/admissions/{admission.Id}", dto);
        }

        [HttpPut("admissions/{id}")]
        public async Task<IActionResult> UpdateAdmission(int id, UpdateAdmissionDto dto)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null) return NotFound();

            admission.AdmissionDate = dto.AdmissionDate;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("admissions/{id}")]
        public async Task<IActionResult> DeleteAdmission(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null) return NotFound();

            _context.Admissions.Remove(admission);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── DISCHARGES ─────────────

        [HttpGet("discharges")]
        public async Task<IActionResult> GetAllDischarges()
        {
            var discharges = await _context.Discharges
                .Select(d => new DischargeDto
                {
                    Id = d.Id, PatientId = d.PatientId, AdmissionId = d.AdmissionId,
                    DischargeDate = d.DischargeDate, DischargeNotes = d.DischargeNotes,
                    PatientName = d.Patient!.FullName
                }).ToListAsync();

            return Ok(discharges);
        }

        [HttpGet("discharges/{id}")]
        public async Task<IActionResult> GetDischargeById(int id)
        {
            var discharge = await _context.Discharges
                .Where(d => d.Id == id)
                .Select(d => new DischargeDto
                {
                    Id = d.Id, PatientId = d.PatientId, AdmissionId = d.AdmissionId,
                    DischargeDate = d.DischargeDate, DischargeNotes = d.DischargeNotes,
                    PatientName = d.Patient!.FullName
                }).FirstOrDefaultAsync();

            if (discharge == null) return NotFound();
            return Ok(discharge);
        }

        [HttpPost("discharges")]
        public async Task<IActionResult> CreateDischarge(CreateDischargeDto dto)
        {
            var discharge = new Discharge
            {
                PatientId = dto.PatientId, AdmissionId = dto.AdmissionId,
                DischargeDate = dto.DischargeDate, DischargeNotes = dto.DischargeNotes
            };

            _context.Discharges.Add(discharge);
            await _context.SaveChangesAsync();
            return Created($"api/appointments/discharges/{discharge.Id}", dto);
        }

        [HttpPut("discharges/{id}")]
        public async Task<IActionResult> UpdateDischarge(int id, UpdateDischargeDto dto)
        {
            var discharge = await _context.Discharges.FindAsync(id);
            if (discharge == null) return NotFound();

            discharge.DischargeDate = dto.DischargeDate;
            discharge.DischargeNotes = dto.DischargeNotes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("discharges/{id}")]
        public async Task<IActionResult> DeleteDischarge(int id)
        {
            var discharge = await _context.Discharges.FindAsync(id);
            if (discharge == null) return NotFound();

            _context.Discharges.Remove(discharge);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

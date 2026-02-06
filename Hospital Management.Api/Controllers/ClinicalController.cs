using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.DTOs.Prescriptions;
using Hospital_Management.Api.DTOs.Labs;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Prescriptions;
using Hospital_Management.Api.Models.Labs;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    public class ClinicalController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public ClinicalController(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── PRESCRIPTIONS ─────────────

        [HttpGet("prescriptions")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = await _context.Prescriptions
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id, PatientId = p.PatientId, DoctorId = p.DoctorId,
                    DateIssued = p.DateIssued,
                    PatientName = p.Patient!.FullName, DoctorName = p.Doctor!.FullName
                }).ToListAsync();

            return Ok(prescriptions);
        }

        [HttpGet("prescriptions/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var prescription = await _context.Prescriptions
                .Where(p => p.Id == id)
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id, PatientId = p.PatientId, DoctorId = p.DoctorId,
                    DateIssued = p.DateIssued,
                    PatientName = p.Patient!.FullName, DoctorName = p.Doctor!.FullName
                }).FirstOrDefaultAsync();

            if (prescription == null) return NotFound();
            return Ok(prescription);
        }

        [HttpPost("prescriptions")]
        public async Task<IActionResult> CreatePrescription(CreatePrescriptionDto dto)
        {
            var prescription = new Prescription
            {
                PatientId = dto.PatientId, DoctorId = dto.DoctorId, DateIssued = dto.DateIssued
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return Created($"api/clinical/prescriptions/{prescription.Id}", dto);
        }

        [HttpPut("prescriptions/{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, UpdatePrescriptionDto dto)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return NotFound();

            prescription.PatientId = dto.PatientId;
            prescription.DoctorId = dto.DoctorId;
            prescription.DateIssued = dto.DateIssued;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("prescriptions/{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return NotFound();

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── PRESCRIPTION ITEMS ─────────────

        [HttpGet("prescriptions/{prescriptionId}/items")]
        public async Task<IActionResult> GetPrescriptionItems(int prescriptionId)
        {
            var items = await _context.PrescriptionItems
                .Where(pi => pi.PrescriptionId == prescriptionId)
                .Select(pi => new PrescriptionItemDto
                {
                    Id = pi.Id, PrescriptionId = pi.PrescriptionId,
                    MedicineId = pi.MedicineId, Dosage = pi.Dosage,
                    DurationDays = pi.DurationDays, MedicineName = pi.Medicine!.Name
                }).ToListAsync();

            return Ok(items);
        }

        [HttpPost("prescriptions/{prescriptionId}/items")]
        public async Task<IActionResult> CreatePrescriptionItem(int prescriptionId, CreatePrescriptionItemDto dto)
        {
            var item = new PrescriptionItem
            {
                PrescriptionId = prescriptionId, MedicineId = dto.MedicineId,
                Dosage = dto.Dosage, DurationDays = dto.DurationDays
            };

            _context.PrescriptionItems.Add(item);
            await _context.SaveChangesAsync();
            return Created($"api/clinical/prescriptions/{prescriptionId}/items", dto);
        }

        [HttpPut("prescription-items/{itemId}")]
        public async Task<IActionResult> UpdatePrescriptionItem(int itemId, UpdatePrescriptionItemDto dto)
        {
            var item = await _context.PrescriptionItems.FindAsync(itemId);
            if (item == null) return NotFound();

            item.MedicineId = dto.MedicineId; item.Dosage = dto.Dosage;
            item.DurationDays = dto.DurationDays;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("prescription-items/{itemId}")]
        public async Task<IActionResult> DeletePrescriptionItem(int itemId)
        {
            var item = await _context.PrescriptionItems.FindAsync(itemId);
            if (item == null) return NotFound();

            _context.PrescriptionItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── MEDICINES ─────────────

        [HttpGet("medicines")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _context.Medicines
                .Select(m => new MedicineDto { Id = m.Id, Name = m.Name })
                .ToListAsync();

            return Ok(medicines);
        }

        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == id)
                .Select(m => new MedicineDto { Id = m.Id, Name = m.Name })
                .FirstOrDefaultAsync();

            if (medicine == null) return NotFound();
            return Ok(medicine);
        }

        [HttpPost("medicines")]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDto dto)
        {
            var medicine = new Medicine { Name = dto.Name };
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return Created($"api/clinical/medicines/{medicine.Id}", new MedicineDto { Id = medicine.Id, Name = medicine.Name });
        }

        [HttpPut("medicines/{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, UpdateMedicineDto dto)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return NotFound();

            medicine.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("medicines/{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return NotFound();

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── LAB TESTS ─────────────

        [HttpGet("lab-tests")]
        public async Task<IActionResult> GetAllLabTests()
        {
            var tests = await _context.LabTests
                .Select(t => new LabTestDto { Id = t.Id, Name = t.Name })
                .ToListAsync();

            return Ok(tests);
        }

        [HttpGet("lab-tests/{id}")]
        public async Task<IActionResult> GetLabTestById(int id)
        {
            var test = await _context.LabTests
                .Where(t => t.Id == id)
                .Select(t => new LabTestDto { Id = t.Id, Name = t.Name })
                .FirstOrDefaultAsync();

            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpPost("lab-tests")]
        public async Task<IActionResult> CreateLabTest(CreateLabTestDto dto)
        {
            var test = new LabTest { Name = dto.Name };
            _context.LabTests.Add(test);
            await _context.SaveChangesAsync();
            return Created($"api/clinical/lab-tests/{test.Id}", new LabTestDto { Id = test.Id, Name = test.Name });
        }

        [HttpPut("lab-tests/{id}")]
        public async Task<IActionResult> UpdateLabTest(int id, UpdateLabTestDto dto)
        {
            var test = await _context.LabTests.FindAsync(id);
            if (test == null) return NotFound();

            test.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("lab-tests/{id}")]
        public async Task<IActionResult> DeleteLabTest(int id)
        {
            var test = await _context.LabTests.FindAsync(id);
            if (test == null) return NotFound();

            _context.LabTests.Remove(test);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── LAB RESULTS ─────────────

        [HttpGet("lab-results")]
        public async Task<IActionResult> GetAllLabResults()
        {
            var results = await _context.LabResults
                .Select(r => new LabResultDto
                {
                    Id = r.Id, PatientId = r.PatientId, LabTestId = r.LabTestId,
                    Result = r.Result, TestDate = r.TestDate,
                    PatientName = r.Patient!.FullName, LabTestName = r.LabTest!.Name
                }).ToListAsync();

            return Ok(results);
        }

        [HttpGet("lab-results/{id}")]
        public async Task<IActionResult> GetLabResultById(int id)
        {
            var result = await _context.LabResults
                .Where(r => r.Id == id)
                .Select(r => new LabResultDto
                {
                    Id = r.Id, PatientId = r.PatientId, LabTestId = r.LabTestId,
                    Result = r.Result, TestDate = r.TestDate,
                    PatientName = r.Patient!.FullName, LabTestName = r.LabTest!.Name
                }).FirstOrDefaultAsync();

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("lab-results")]
        public async Task<IActionResult> CreateLabResult(CreateLabResultDto dto)
        {
            var result = new LabResult
            {
                PatientId = dto.PatientId, LabTestId = dto.LabTestId,
                Result = dto.Result, TestDate = dto.TestDate
            };

            _context.LabResults.Add(result);
            await _context.SaveChangesAsync();
            return Created($"api/clinical/lab-results/{result.Id}", dto);
        }

        [HttpPut("lab-results/{id}")]
        public async Task<IActionResult> UpdateLabResult(int id, UpdateLabResultDto dto)
        {
            var result = await _context.LabResults.FindAsync(id);
            if (result == null) return NotFound();

            result.Result = dto.Result; result.TestDate = dto.TestDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("lab-results/{id}")]
        public async Task<IActionResult> DeleteLabResult(int id)
        {
            var result = await _context.LabResults.FindAsync(id);
            if (result == null) return NotFound();

            _context.LabResults.Remove(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Api.DTOs.Prescriptions;
using Hospital_Management.Api.DTOs.Labs;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Prescriptions;
using Hospital_Management.Api.Models.Labs;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    [Authorize(Roles = "Admin,Doctor")]
    public class ClinicalController : ControllerBase
    {
        private readonly IClinicalRepository _repo;

        public ClinicalController(IClinicalRepository repo)
        {
            _repo = repo;
        }

        // ───────────── PRESCRIPTIONS ─────────────

        [HttpGet("prescriptions")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = (await _repo.GetAllPrescriptionsAsync())
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id, PatientId = p.PatientId, DoctorId = p.DoctorId,
                    DateIssued = p.DateIssued,
                    PatientName = p.Patient!.FullName, DoctorName = p.Doctor!.FullName
                }).ToList();

            return Ok(prescriptions);
        }

        [HttpGet("prescriptions/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var p = await _repo.GetPrescriptionByIdAsync(id);
            if (p == null) return NotFound();

            return Ok(new PrescriptionDto
            {
                Id = p.Id, PatientId = p.PatientId, DoctorId = p.DoctorId,
                DateIssued = p.DateIssued,
                PatientName = p.Patient!.FullName, DoctorName = p.Doctor!.FullName
            });
        }

        [HttpPost("prescriptions")]
        public async Task<IActionResult> CreatePrescription(CreatePrescriptionDto dto)
        {
            var prescription = new Prescription
            {
                PatientId = dto.PatientId, DoctorId = dto.DoctorId, DateIssued = dto.DateIssued
            };

            await _repo.AddPrescriptionAsync(prescription);
            return Created($"api/clinical/prescriptions/{prescription.Id}", dto);
        }

        [HttpPut("prescriptions/{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, UpdatePrescriptionDto dto)
        {
            var prescription = await _repo.GetPrescriptionByIdAsync(id);
            if (prescription == null) return NotFound();

            prescription.PatientId = dto.PatientId;
            prescription.DoctorId = dto.DoctorId;
            prescription.DateIssued = dto.DateIssued;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("prescriptions/{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var deleted = await _repo.DeletePrescriptionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── PRESCRIPTION ITEMS ─────────────

        [HttpGet("prescriptions/{prescriptionId}/items")]
        public async Task<IActionResult> GetPrescriptionItems(int prescriptionId)
        {
            var items = (await _repo.GetPrescriptionItemsByPrescriptionIdAsync(prescriptionId))
                .Select(pi => new PrescriptionItemDto
                {
                    Id = pi.Id, PrescriptionId = pi.PrescriptionId,
                    MedicineId = pi.MedicineId, Dosage = pi.Dosage,
                    DurationDays = pi.DurationDays, MedicineName = pi.Medicine!.Name
                }).ToList();

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

            await _repo.AddPrescriptionItemAsync(item);
            return Created($"api/clinical/prescriptions/{prescriptionId}/items", dto);
        }

        [HttpPut("prescription-items/{itemId}")]
        public async Task<IActionResult> UpdatePrescriptionItem(int itemId, UpdatePrescriptionItemDto dto)
        {
            var item = await _repo.GetPrescriptionItemByIdAsync(itemId);
            if (item == null) return NotFound();

            item.MedicineId = dto.MedicineId; item.Dosage = dto.Dosage;
            item.DurationDays = dto.DurationDays;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("prescription-items/{itemId}")]
        public async Task<IActionResult> DeletePrescriptionItem(int itemId)
        {
            var deleted = await _repo.DeletePrescriptionItemAsync(itemId);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── MEDICINES ─────────────

        [HttpGet("medicines")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = (await _repo.GetAllMedicinesAsync())
                .Select(m => new MedicineDto { Id = m.Id, Name = m.Name })
                .ToList();

            return Ok(medicines);
        }

        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var medicine = await _repo.GetMedicineByIdAsync(id);
            if (medicine == null) return NotFound();
            return Ok(new MedicineDto { Id = medicine.Id, Name = medicine.Name });
        }

        [HttpPost("medicines")]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDto dto)
        {
            var medicine = new Medicine { Name = dto.Name };
            await _repo.AddMedicineAsync(medicine);
            return Created($"api/clinical/medicines/{medicine.Id}", new MedicineDto { Id = medicine.Id, Name = medicine.Name });
        }

        [HttpPut("medicines/{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, UpdateMedicineDto dto)
        {
            var medicine = await _repo.GetMedicineByIdAsync(id);
            if (medicine == null) return NotFound();

            medicine.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("medicines/{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var deleted = await _repo.DeleteMedicineAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── LAB TESTS ─────────────

        [HttpGet("lab-tests")]
        public async Task<IActionResult> GetAllLabTests()
        {
            var tests = (await _repo.GetAllLabTestsAsync())
                .Select(t => new LabTestDto { Id = t.Id, Name = t.Name })
                .ToList();

            return Ok(tests);
        }

        [HttpGet("lab-tests/{id}")]
        public async Task<IActionResult> GetLabTestById(int id)
        {
            var test = await _repo.GetLabTestByIdAsync(id);
            if (test == null) return NotFound();
            return Ok(new LabTestDto { Id = test.Id, Name = test.Name });
        }

        [HttpPost("lab-tests")]
        public async Task<IActionResult> CreateLabTest(CreateLabTestDto dto)
        {
            var test = new LabTest { Name = dto.Name };
            await _repo.AddLabTestAsync(test);
            return Created($"api/clinical/lab-tests/{test.Id}", new LabTestDto { Id = test.Id, Name = test.Name });
        }

        [HttpPut("lab-tests/{id}")]
        public async Task<IActionResult> UpdateLabTest(int id, UpdateLabTestDto dto)
        {
            var test = await _repo.GetLabTestByIdAsync(id);
            if (test == null) return NotFound();

            test.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("lab-tests/{id}")]
        public async Task<IActionResult> DeleteLabTest(int id)
        {
            var deleted = await _repo.DeleteLabTestAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── LAB RESULTS ─────────────

        [HttpGet("lab-results")]
        public async Task<IActionResult> GetAllLabResults()
        {
            var results = (await _repo.GetAllLabResultsAsync())
                .Select(r => new LabResultDto
                {
                    Id = r.Id, PatientId = r.PatientId, LabTestId = r.LabTestId,
                    Result = r.Result, TestDate = r.TestDate,
                    PatientName = r.Patient!.FullName, LabTestName = r.LabTest!.Name
                }).ToList();

            return Ok(results);
        }

        [HttpGet("lab-results/{id}")]
        public async Task<IActionResult> GetLabResultById(int id)
        {
            var r = await _repo.GetLabResultByIdAsync(id);
            if (r == null) return NotFound();

            return Ok(new LabResultDto
            {
                Id = r.Id, PatientId = r.PatientId, LabTestId = r.LabTestId,
                Result = r.Result, TestDate = r.TestDate,
                PatientName = r.Patient!.FullName, LabTestName = r.LabTest!.Name
            });
        }

        [HttpPost("lab-results")]
        public async Task<IActionResult> CreateLabResult(CreateLabResultDto dto)
        {
            var result = new LabResult
            {
                PatientId = dto.PatientId, LabTestId = dto.LabTestId,
                Result = dto.Result, TestDate = dto.TestDate
            };

            await _repo.AddLabResultAsync(result);
            return Created($"api/clinical/lab-results/{result.Id}", dto);
        }

        [HttpPut("lab-results/{id}")]
        public async Task<IActionResult> UpdateLabResult(int id, UpdateLabResultDto dto)
        {
            var result = await _repo.GetLabResultByIdAsync(id);
            if (result == null) return NotFound();

            result.Result = dto.Result; result.TestDate = dto.TestDate;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("lab-results/{id}")]
        public async Task<IActionResult> DeleteLabResult(int id)
        {
            var deleted = await _repo.DeleteLabResultAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

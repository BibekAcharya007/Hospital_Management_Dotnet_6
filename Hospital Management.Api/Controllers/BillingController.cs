using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Api.DTOs.Billing;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Billing;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    [Authorize(Roles = "Admin,Doctor")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingRepository _repo;

        public BillingController(IBillingRepository repo)
        {
            _repo = repo;
        }

        // ───────────── BILLS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var bills = (await _repo.GetAllBillsAsync())
                .Select(b => new BillDto
                {
                    Id = b.Id, PatientId = b.PatientId,
                    TotalAmount = b.TotalAmount, IsPaid = b.IsPaid,
                    PatientName = b.Patient!.FullName
                }).ToList();

            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(int id)
        {
            var b = await _repo.GetBillByIdAsync(id);
            if (b == null) return NotFound();

            return Ok(new BillDto
            {
                Id = b.Id, PatientId = b.PatientId,
                TotalAmount = b.TotalAmount, IsPaid = b.IsPaid,
                PatientName = b.Patient!.FullName
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(CreateBillDto dto)
        {
            var bill = new Bill
            {
                PatientId = dto.PatientId, TotalAmount = dto.TotalAmount, IsPaid = dto.IsPaid
            };

            await _repo.AddBillAsync(bill);
            return CreatedAtAction(nameof(GetBillById), new { id = bill.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, UpdateBillDto dto)
        {
            var bill = await _repo.GetBillByIdAsync(id);
            if (bill == null) return NotFound();

            bill.TotalAmount = dto.TotalAmount; bill.IsPaid = dto.IsPaid;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var deleted = await _repo.DeleteBillAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── BILL ITEMS ─────────────

        [HttpGet("{billId}/items")]
        public async Task<IActionResult> GetBillItems(int billId)
        {
            var items = (await _repo.GetBillItemsByBillIdAsync(billId))
                .Select(bi => new BillItemDto
                {
                    Id = bi.Id, BillId = bi.BillId,
                    Description = bi.Description, Amount = bi.Amount
                }).ToList();

            return Ok(items);
        }

        [HttpPost("{billId}/items")]
        public async Task<IActionResult> CreateBillItem(int billId, CreateBillItemDto dto)
        {
            var item = new BillItem
            {
                BillId = billId, Description = dto.Description, Amount = dto.Amount
            };

            await _repo.AddBillItemAsync(item);
            return Created($"api/billing/{billId}/items", dto);
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateBillItem(int itemId, UpdateBillItemDto dto)
        {
            var item = await _repo.GetBillItemByIdAsync(itemId);
            if (item == null) return NotFound();

            item.Description = dto.Description; item.Amount = dto.Amount;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteBillItem(int itemId)
        {
            var deleted = await _repo.DeleteBillItemAsync(itemId);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── INSURANCE CLAIMS ─────────────

        [HttpGet("{billId}/insurance")]
        public async Task<IActionResult> GetInsuranceClaim(int billId)
        {
            var ic = await _repo.GetInsuranceClaimByBillIdAsync(billId);
            if (ic == null) return NotFound();

            return Ok(new InsuranceClaimDto
            {
                Id = ic.Id, BillId = ic.BillId,
                ProviderName = ic.ProviderName, Status = ic.Status
            });
        }

        [HttpPost("{billId}/insurance")]
        public async Task<IActionResult> CreateInsuranceClaim(int billId, CreateInsuranceClaimDto dto)
        {
            var claim = new InsuranceClaim
            {
                BillId = billId, ProviderName = dto.ProviderName, Status = dto.Status
            };

            await _repo.AddInsuranceClaimAsync(claim);
            return Created($"api/billing/{billId}/insurance", dto);
        }

        [HttpPut("insurance/{claimId}")]
        public async Task<IActionResult> UpdateInsuranceClaim(int claimId, UpdateInsuranceClaimDto dto)
        {
            var claim = await _repo.GetInsuranceClaimByIdAsync(claimId);
            if (claim == null) return NotFound();

            claim.ProviderName = dto.ProviderName; claim.Status = dto.Status;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("insurance/{claimId}")]
        public async Task<IActionResult> DeleteInsuranceClaim(int claimId)
        {
            var deleted = await _repo.DeleteInsuranceClaimAsync(claimId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

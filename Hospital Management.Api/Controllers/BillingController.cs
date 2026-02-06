using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.DTOs.Billing;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Billing;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    public class BillingController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public BillingController(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── BILLS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var bills = await _context.Bills
                .Select(b => new BillDto
                {
                    Id = b.Id, PatientId = b.PatientId,
                    TotalAmount = b.TotalAmount, IsPaid = b.IsPaid,
                    PatientName = b.Patient!.FullName
                }).ToListAsync();

            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(int id)
        {
            var bill = await _context.Bills
                .Where(b => b.Id == id)
                .Select(b => new BillDto
                {
                    Id = b.Id, PatientId = b.PatientId,
                    TotalAmount = b.TotalAmount, IsPaid = b.IsPaid,
                    PatientName = b.Patient!.FullName
                }).FirstOrDefaultAsync();

            if (bill == null) return NotFound();
            return Ok(bill);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(CreateBillDto dto)
        {
            var bill = new Bill
            {
                PatientId = dto.PatientId, TotalAmount = dto.TotalAmount, IsPaid = dto.IsPaid
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBillById), new { id = bill.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, UpdateBillDto dto)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null) return NotFound();

            bill.TotalAmount = dto.TotalAmount; bill.IsPaid = dto.IsPaid;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null) return NotFound();

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── BILL ITEMS ─────────────

        [HttpGet("{billId}/items")]
        public async Task<IActionResult> GetBillItems(int billId)
        {
            var items = await _context.BillItems
                .Where(bi => bi.BillId == billId)
                .Select(bi => new BillItemDto
                {
                    Id = bi.Id, BillId = bi.BillId,
                    Description = bi.Description, Amount = bi.Amount
                }).ToListAsync();

            return Ok(items);
        }

        [HttpPost("{billId}/items")]
        public async Task<IActionResult> CreateBillItem(int billId, CreateBillItemDto dto)
        {
            var item = new BillItem
            {
                BillId = billId, Description = dto.Description, Amount = dto.Amount
            };

            _context.BillItems.Add(item);
            await _context.SaveChangesAsync();
            return Created($"api/billing/{billId}/items", dto);
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateBillItem(int itemId, UpdateBillItemDto dto)
        {
            var item = await _context.BillItems.FindAsync(itemId);
            if (item == null) return NotFound();

            item.Description = dto.Description; item.Amount = dto.Amount;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteBillItem(int itemId)
        {
            var item = await _context.BillItems.FindAsync(itemId);
            if (item == null) return NotFound();

            _context.BillItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── INSURANCE CLAIMS ─────────────

        [HttpGet("{billId}/insurance")]
        public async Task<IActionResult> GetInsuranceClaim(int billId)
        {
            var claim = await _context.InsuranceClaims
                .Where(ic => ic.BillId == billId)
                .Select(ic => new InsuranceClaimDto
                {
                    Id = ic.Id, BillId = ic.BillId,
                    ProviderName = ic.ProviderName, Status = ic.Status
                }).FirstOrDefaultAsync();

            if (claim == null) return NotFound();
            return Ok(claim);
        }

        [HttpPost("{billId}/insurance")]
        public async Task<IActionResult> CreateInsuranceClaim(int billId, CreateInsuranceClaimDto dto)
        {
            var claim = new InsuranceClaim
            {
                BillId = billId, ProviderName = dto.ProviderName, Status = dto.Status
            };

            _context.InsuranceClaims.Add(claim);
            await _context.SaveChangesAsync();
            return Created($"api/billing/{billId}/insurance", dto);
        }

        [HttpPut("insurance/{claimId}")]
        public async Task<IActionResult> UpdateInsuranceClaim(int claimId, UpdateInsuranceClaimDto dto)
        {
            var claim = await _context.InsuranceClaims.FindAsync(claimId);
            if (claim == null) return NotFound();

            claim.ProviderName = dto.ProviderName; claim.Status = dto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("insurance/{claimId}")]
        public async Task<IActionResult> DeleteInsuranceClaim(int claimId)
        {
            var claim = await _context.InsuranceClaims.FindAsync(claimId);
            if (claim == null) return NotFound();

            _context.InsuranceClaims.Remove(claim);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Billing;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class BillingRepository : IBillingRepository
    {
        private readonly HospitalDbContext _context;

        public BillingRepository(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── BILLS ─────────────

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
            => await _context.Bills
                .Include(b => b.Patient)
                .ToListAsync();

        public async Task<Bill?> GetBillByIdAsync(int id)
            => await _context.Bills
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task AddBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateBillAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBillAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null) return false;
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── BILL ITEMS ─────────────

        public async Task<IEnumerable<BillItem>> GetBillItemsByBillIdAsync(int billId)
            => await _context.BillItems.Where(bi => bi.BillId == billId).ToListAsync();

        public async Task AddBillItemAsync(BillItem item)
        {
            _context.BillItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<BillItem?> GetBillItemByIdAsync(int itemId)
            => await _context.BillItems.FindAsync(itemId);

        public async Task<bool> UpdateBillItemAsync(BillItem item)
        {
            _context.BillItems.Update(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBillItemAsync(int itemId)
        {
            var item = await _context.BillItems.FindAsync(itemId);
            if (item == null) return false;
            _context.BillItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── INSURANCE CLAIMS ─────────────

        public async Task<InsuranceClaim?> GetInsuranceClaimByBillIdAsync(int billId)
            => await _context.InsuranceClaims.FirstOrDefaultAsync(ic => ic.BillId == billId);

        public async Task AddInsuranceClaimAsync(InsuranceClaim claim)
        {
            _context.InsuranceClaims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<InsuranceClaim?> GetInsuranceClaimByIdAsync(int claimId)
            => await _context.InsuranceClaims.FindAsync(claimId);

        public async Task<bool> UpdateInsuranceClaimAsync(InsuranceClaim claim)
        {
            _context.InsuranceClaims.Update(claim);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInsuranceClaimAsync(int claimId)
        {
            var claim = await _context.InsuranceClaims.FindAsync(claimId);
            if (claim == null) return false;
            _context.InsuranceClaims.Remove(claim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}

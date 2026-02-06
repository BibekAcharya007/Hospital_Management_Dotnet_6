using Hospital_Management.Api.Models.Billing;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IBillingRepository
    {
        // Bills
        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task<Bill?> GetBillByIdAsync(int id);
        Task AddBillAsync(Bill bill);
        Task<bool> UpdateBillAsync(Bill bill);
        Task<bool> DeleteBillAsync(int id);

        // Bill Items
        Task<IEnumerable<BillItem>> GetBillItemsByBillIdAsync(int billId);
        Task AddBillItemAsync(BillItem item);
        Task<BillItem?> GetBillItemByIdAsync(int itemId);
        Task<bool> UpdateBillItemAsync(BillItem item);
        Task<bool> DeleteBillItemAsync(int itemId);

        // Insurance Claims
        Task<InsuranceClaim?> GetInsuranceClaimByBillIdAsync(int billId);
        Task AddInsuranceClaimAsync(InsuranceClaim claim);
        Task<InsuranceClaim?> GetInsuranceClaimByIdAsync(int claimId);
        Task<bool> UpdateInsuranceClaimAsync(InsuranceClaim claim);
        Task<bool> DeleteInsuranceClaimAsync(int claimId);

        Task SaveChangesAsync();
    }
}

using BusinessObjects.Entities;

namespace DataAccess {
    public interface IHrAccountRepository {
        public Task<List<HrAccount>> GetHrAccountsAsync();
        public Task<HrAccount?> GetHrAccountAsync(string email, string password);
        Task AddHrAccountAsync(HrAccount hrAccount);
        Task UpdateHrAccountAsync(HrAccount hrAccount);
        Task RemoveHrAccountAsync(HrAccount hrAccount);
    }
}
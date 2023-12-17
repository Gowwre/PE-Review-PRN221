using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess {
    public class HrAccountRepository : IHrAccountRepository {
        private readonly CandidateManagementContext _context;

        public HrAccountRepository() {
            _context = new CandidateManagementContext();
        }

        public Task<List<HrAccount>> GetHrAccountsAsync() {
            Task<List<HrAccount>> result = _context.Hraccounts.ToListAsync();
            return result;
        }

        public Task<HrAccount?> GetHrAccountAsync(string email, string password) {
            var result = _context.Hraccounts.Where(x => x.Email == email && x.Password == password)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task AddHrAccountAsync(HrAccount hrAccount) {
            _context.Hraccounts.Add(hrAccount);
            return _context.SaveChangesAsync();
        }

        public Task UpdateHrAccountAsync(HrAccount hrAccount) {
            _context.Hraccounts.Update(hrAccount);
            return _context.SaveChangesAsync();
        }

        public Task RemoveHrAccountAsync(HrAccount hrAccount) {
            _context.Hraccounts.Remove(hrAccount);
            return _context.SaveChangesAsync();
        }
    }
}
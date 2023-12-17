using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess {
    public class JobPostingRepository : IJobPostingRepository {
        private readonly CandidateManagementContext _context;

        public JobPostingRepository() {
            _context = new CandidateManagementContext();
        }

        public Task<List<JobPosting>> GetJobPostingsAsync() {
            return _context.JobPostings.ToListAsync();
        }
    }
}
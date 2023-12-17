using BusinessObjects.Entities;

namespace DataAccess {
    public interface IJobPostingRepository {
        Task<List<JobPosting>> GetJobPostingsAsync();
    }
}
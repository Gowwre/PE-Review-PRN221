using BusinessObjects.DTO;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess {
    public class CandidateProfileRepository : ICandidateProfileRepository {
        private readonly CandidateManagementContext _context;

        public CandidateProfileRepository() {
            _context = new CandidateManagementContext();
        }

        public Task<List<CandidateProfile>> GetCandidateProfilesAsync() {
            Task<List<CandidateProfile>> result = _context.CandidateProfiles.ToListAsync();
            return result;
        }

        public Task AddCandidateProfileAsync(CandidateProfile candidateProfile) {
            _context.CandidateProfiles.Add(candidateProfile);
            return _context.SaveChangesAsync();
        }

        public Task<CandidateProfile> GetCandidateProfileAsync(string id) {
            Task<CandidateProfile?> result = _context.CandidateProfiles.FirstOrDefaultAsync(x => x.CandidateId == id);
            return result;
        }

        public Task RemoveCandidateProfile(CandidateProfile candidateProfile) {
            _context.CandidateProfiles.Remove(candidateProfile);
            return _context.SaveChangesAsync();
        }

        
        public Task UpdateCandidateProfileAsync(CandidateProfile candidateProfile) {
            _context.CandidateProfiles.Update(candidateProfile);
            return _context.SaveChangesAsync();
        }

        
        public Task<List<CandidateProfile>> GetCandidateProfilesAsync(SearchParam param) {
            var keywordName = "%" + param.Name + "%";
            var keywordId = "%" + param.Id + "%";
            var currentPage = param.Page;
            var pageSize = param.PageSize;

            var query = _context.CandidateProfiles.AsQueryable();

            query = query.Where(x => EF.Functions.Like(x.Fullname, keywordName) || EF.Functions.Like(x.CandidateId, keywordId));

            query = query.Skip((currentPage.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

            var result = query.ToListAsync();
            return result;
        }

        public Task<int> GetCandidateProfileCountAsync(SearchParam param) {
            var keywordName = "%" + param.Name + "%";
            var keywordId = "%" + param.Id + "%";

            var query = _context.CandidateProfiles.AsQueryable();

            query = query.Where(x => EF.Functions.Like(x.Fullname, keywordName) || EF.Functions.Like(x.CandidateId, keywordId));

            var result = query.CountAsync();
            return result;
        }
    }
}
using BusinessObjects.DTO;
using BusinessObjects.Entities;

namespace DataAccess {
    public interface ICandidateProfileRepository {
        public Task<List<CandidateProfile>> GetCandidateProfilesAsync();
        public Task AddCandidateProfileAsync(CandidateProfile candidateProfile);
        public Task<CandidateProfile> GetCandidateProfileAsync(string id);
        public Task RemoveCandidateProfile(CandidateProfile candidateProfile);
        public Task UpdateCandidateProfileAsync(CandidateProfile candidateProfile);
        public Task<List<CandidateProfile>> GetCandidateProfilesAsync(SearchParam param);
        public Task<int> GetCandidateProfileCountAsync(SearchParam param);
    }
}
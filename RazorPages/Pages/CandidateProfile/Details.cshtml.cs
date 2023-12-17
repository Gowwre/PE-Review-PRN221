using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.CandidateProfile {
    public class DetailsModel : PageModel {
        private readonly ICandidateProfileRepository _candidateProfileRepository;

        public DetailsModel(ICandidateProfileRepository candidateProfileRepository) {
            _candidateProfileRepository = candidateProfileRepository;
        }

        public BusinessObjects.Entities.CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id) {
            if (id == null || await _candidateProfileRepository.GetCandidateProfilesAsync() == null) {
                return NotFound();
            }

            BusinessObjects.Entities.CandidateProfile? candidateprofile =
                await _candidateProfileRepository.GetCandidateProfileAsync(id);
            if (candidateprofile == null) {
                return NotFound();
            }

            CandidateProfile = candidateprofile;

            return Page();
        }
    }
}
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.CandidateProfile {
    public class DeleteModel : PageModel {
        private readonly ICandidateProfileRepository _candidateProfileRepository;

        public DeleteModel(ICandidateProfileRepository candidateProfileRepository) {
            _candidateProfileRepository = candidateProfileRepository;
        }

        [BindProperty] public BusinessObjects.Entities.CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id) {
            BusinessObjects.Entities.CandidateProfile candidateProfile =
                await _candidateProfileRepository.GetCandidateProfileAsync(id);
            CandidateProfile = candidateProfile;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id) {
            BusinessObjects.Entities.CandidateProfile candidateProfile =
                await _candidateProfileRepository.GetCandidateProfileAsync(id);

            await _candidateProfileRepository.RemoveCandidateProfile(candidateProfile);

            return RedirectToPage("./Index");
        }
    }
}
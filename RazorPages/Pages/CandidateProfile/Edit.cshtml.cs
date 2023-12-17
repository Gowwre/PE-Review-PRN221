using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPages.Pages.CandidateProfile {
    public class EditModel : PageModel {
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly IJobPostingRepository _jobPostingRepository;

        public EditModel(ICandidateProfileRepository candidateProfileRepository,
            IJobPostingRepository jobPostingRepository) {
            _candidateProfileRepository = candidateProfileRepository;
            _jobPostingRepository = jobPostingRepository;
        }

        [BindProperty] public BusinessObjects.Entities.CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id) {
            var candidateProfileList = await _candidateProfileRepository.GetCandidateProfilesAsync();

            if (id == null || candidateProfileList == null) {
                return NotFound();
            }

            BusinessObjects.Entities.CandidateProfile? candidateprofile =
                await _candidateProfileRepository.GetCandidateProfileAsync(id);
            if (candidateprofile == null) {
                return NotFound();
            }

            CandidateProfile = candidateprofile;
            var jobPostings = await _jobPostingRepository.GetJobPostingsAsync();
            ViewData["PostingId"] =
                new SelectList(jobPostings, "PostingId", "PostingId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            await _candidateProfileRepository.UpdateCandidateProfileAsync(CandidateProfile);
            return RedirectToPage("./Index");
        }
    }
}
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPages.Pages.CandidateProfile {
    public class CreateModel : PageModel {
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly IJobPostingRepository _jobPostingRepository;

        public CreateModel(ICandidateProfileRepository candidateProfileRepository,
            IJobPostingRepository jobPostingRepository) {
            _candidateProfileRepository = candidateProfileRepository;
            _jobPostingRepository = jobPostingRepository;
        }

        [BindProperty] public BusinessObjects.Entities.CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGet() {
            ViewData["PostingId"] =
                new SelectList(await _jobPostingRepository.GetJobPostingsAsync(), "PostingId", "PostingId");
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            var candidateProfileList = await _candidateProfileRepository.GetCandidateProfilesAsync();
            if (!ModelState.IsValid || candidateProfileList  == null ||
                CandidateProfile == null) {
                return Page();
            }

            await _candidateProfileRepository.AddCandidateProfileAsync(CandidateProfile);
            return RedirectToPage("./Index");
        }
    }
}
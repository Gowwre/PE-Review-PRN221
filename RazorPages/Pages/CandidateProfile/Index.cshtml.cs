using BusinessObjects.DTO;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.CandidateProfile {
    public class IndexModel : PageModel {
        private readonly ICandidateProfileRepository _candidateProfileRepository;

        public IndexModel(ICandidateProfileRepository candidateProfileRepository) {
            _candidateProfileRepository = candidateProfileRepository;
        }

        public IList<BusinessObjects.Entities.CandidateProfile> CandidateProfileList { get; set; } = default!;


        [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)] public string Name { get; set; }

        [BindProperty(SupportsGet = true)] public string Id { get; set; }

        public int PageSize { get; set; } = 4;

        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

        public async Task<IActionResult> OnGetAsync() {
            var currentMemberRole = HttpContext.Session.GetInt32("memberRole");

            if (currentMemberRole == null || currentMemberRole != 1)
                return RedirectToPage("/Login/Index");

            var searchParam = new SearchParam {
                Name = Name,
                Id = Id,
                Page = CurrentPage,
                PageSize = PageSize
            };

            CandidateProfileList = await _candidateProfileRepository.GetCandidateProfilesAsync(searchParam);
            TotalCount = await _candidateProfileRepository.GetCandidateProfileCountAsync(searchParam);
            return Page();
        }
    }
}
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RazorPages.Pages.Login {
    public class IndexModel : PageModel {
        private readonly IHrAccountRepository _hrAccountRepository;

        public IndexModel(IHrAccountRepository hrAccountRepository) {
            _hrAccountRepository = hrAccountRepository;
        }
        
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet() {
        }

        public async Task<IActionResult> OnPostAsync() {
            var email = Email;
            var password = Password;
            
            var account = await _hrAccountRepository.GetHrAccountAsync(email, password);
            
            if (account == null) {
                ViewData["ErrorMessage"] = "Invalid email or password";
                return Page();
            }

            if(account.MemberRole != 1){
                ViewData["ErrorMessage"] = "Insufficient role";
                return Page();
            }

            HttpContext.Session.SetInt32("memberRole", account.MemberRole.Value);

            return RedirectToPage("/CandidateProfile/Index");
        }
    }
}
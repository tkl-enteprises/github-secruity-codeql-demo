using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1202demoapp.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<ContactModel> _logger;

        public ContactModel(ILogger<ContactModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Message { get; set; } = string.Empty;

        public bool SubmitSuccess { get; set; } = false;

        public void OnGet()
        {
            _logger.LogInformation("Contact form page accessed at {Time}", DateTime.UtcNow);
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("Contact form submitted at {Time}", DateTime.UtcNow);

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email))
            {
                _logger.LogWarning("Contact form submission failed: missing required fields");
                return Page();
            }

            SubmitSuccess = true;
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            _logger.LogInformation("Contact delete operation requested for id {Id} at {Time}", id, DateTime.UtcNow);
            return RedirectToPage();
        }

        public IActionResult OnPostApprove(int id)
        {
            _logger.LogInformation("Contact approve operation requested for id {Id} at {Time}", id, DateTime.UtcNow);
            return RedirectToPage();
        }
    }
}

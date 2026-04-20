using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1202demoapp.Pages
{
    public class AAAModel : PageModel
    {
        private readonly ILogger<AAAModel> _logger;

        public AAAModel(ILogger<AAAModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Action { get; set; } = string.Empty;

        public bool ActionPerformed { get; set; } = false;
        public string ResultMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // GOOD PRACTICE: Logging page access for accounting
            _logger.LogInformation("AAA page accessed at {Time}", DateTime.UtcNow);
        }

        public IActionResult OnPost()
        {
            // Authentication: Verify user identity
            if (string.IsNullOrWhiteSpace(Username))
            {
                _logger.LogWarning("AAA: Action attempted without username at {Time}", DateTime.UtcNow);
                ResultMessage = "Authentication failed: Username is required";
                ActionPerformed = true;
                return Page();
            }

            // Accounting: Log the authentication attempt
            _logger.LogInformation("AAA: User {Username} authenticated at {Time}", Username, DateTime.UtcNow);

            // Authorization: Check if user is allowed to perform the action
            if (string.IsNullOrWhiteSpace(Action))
            {
                _logger.LogWarning("AAA: User {Username} attempted action without specifying action type", Username);
                ResultMessage = "Authorization failed: No action specified";
                ActionPerformed = true;
                return Page();
            }

            // Simulate authorization logic
            bool isAuthorized = AuthorizeAction(Username, Action);

            if (!isAuthorized)
            {
                // Accounting: Log authorization failure
                _logger.LogWarning("AAA: User {Username} denied access to action {Action} at {Time}",
                    Username, Action, DateTime.UtcNow);
                ResultMessage = $"Authorization failed: User '{Username}' is not authorized to perform '{Action}'";
                ActionPerformed = true;
                return Page();
            }

            // Accounting: Log successful action
            _logger.LogInformation("AAA: User {Username} successfully performed {Action} at {Time}",
                Username, Action, DateTime.UtcNow);

            ResultMessage = $"Success! User '{Username}' performed '{Action}' (Authentication ✓, Authorization ✓, Accounting ✓)";
            ActionPerformed = true;

            return Page();
        }

        private bool AuthorizeAction(string username, string action)
        {
            // Simple authorization logic for demonstration
            // In a real application, this would check against a database or policy service
            return action switch
            {
                "read" => true, // Everyone can read
                "write" => username.Length > 3, // Only users with longer names can write
                "delete" => username.Equals("admin", StringComparison.OrdinalIgnoreCase), // Only admin can delete
                _ => false
            };
        }
    }
}

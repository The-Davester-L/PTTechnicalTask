using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PinewoodTechnologies.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        public readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Username { get; set; } // User-entered username

        [BindProperty]
        public string Password { get; set; } // User-entered password

        public string ErrorMessage { get; set; } // Error message to display

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Retrieve credentials from configuration
            var configUsername = _configuration["Credentials:Username"];
            var configPassword = _configuration["Credentials:Password"];

            // Validate credentials
            if (Username == configUsername && Password == configPassword)
            {
                // Set session variable to indicate user is logged in
                HttpContext.Session.SetString("IsAuthenticated", "true");

                // Redirect to the Customers page
                return RedirectToPage("/Customers/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}

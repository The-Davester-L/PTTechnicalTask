using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PinewoodTechnologies.UI.Pages.Base;
using PinewoodTechnologies.API.Models;

namespace PinewoodTechnologies.UI.Pages.Customers
{
    public class CreateModel : BasePageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = _configuration["ApiSettings:ApiUrl"];
            var apiKey = _configuration["ApiSettings:ApiKey"];

            using var client = new HttpClient();

            // Include the API key in the header
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);

            var jsonContent = JsonConvert.SerializeObject(Customer);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to the API
            var response = await client.PostAsync($"{apiUrl}/Customers", contentString);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the customer. {response.ReasonPhrase}");
                return Page();
            }
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PinewoodTechnologies.UI.Pages.Base;
using PinewoodTechnologies.API.Models;

namespace PinewoodTechnologies.UI.Pages.Customers
{
    public class DeleteModel : BasePageModel
    {
        private readonly IConfiguration _configuration;

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiUrl = _configuration["ApiSettings:ApiUrl"];
            var apiKey = _configuration["ApiSettings:ApiKey"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);

            // Send GET request to fetch the customer
            var response = await client.GetAsync($"{apiUrl}/Customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Customer = JsonConvert.DeserializeObject<Customer>(jsonResponse);
                return Page();
            }
            else
            {
                // Handle error response
                return RedirectToPage("Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = _configuration["ApiSettings:ApiUrl"];
            var apiKey = _configuration["ApiSettings:ApiKey"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);

            // Send DELETE request to delete the customer
            var response = await client.DeleteAsync($"{apiUrl}/Customers/{Customer.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the customer. {response.ReasonPhrase}");
                return Page();
            }
        }
    }
}

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
    public class EditModel : BasePageModel
    {
        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
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

            var jsonContent = JsonConvert.SerializeObject(Customer);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send PUT request to update the customer
            var response = await client.PutAsync($"{apiUrl}/Customers/{Customer.Id}", contentString);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the customer. {response.ReasonPhrase}");
                return Page();
            }
        }
    }
}

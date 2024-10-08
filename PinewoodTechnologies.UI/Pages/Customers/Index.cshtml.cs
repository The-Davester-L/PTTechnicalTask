using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PinewoodTechnologies.UI.Pages.Base;
using PinewoodTechnologies.API.Models;

namespace PinewoodTechnologies.UI.Pages.Customers
{
    public class IndexModel : BasePageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public async Task OnGetAsync()
        {
            var apiUrl = _configuration["ApiSettings:ApiUrl"];
            var apiKey = _configuration["ApiSettings:ApiKey"];

            using var client = new HttpClient();

            // Include the API key in the header
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);

            // Send GET request to the API
            var response = await client.GetAsync($"{apiUrl}/Customers");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Customers = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse);
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, $"An error occurred while listing the customer. {response.ReasonPhrase}");

                // Handle error response
                Customers = new List<Customer>();
            }
        }
    }
}

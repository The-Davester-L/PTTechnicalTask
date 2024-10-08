using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PinewoodTechnologies.API.Middleware
{
    // Custom middleware to validate API Key
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey"; // Header name where the API key is expected
        private readonly string _apiKey;

        // Constructor to inject the next middleware and access configuration settings
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            // Retrieve the API key from configuration (appsettings.json)
            _apiKey = configuration["ApiSettings:ApiKey"];
        }

        // Method that gets called on every HTTP request
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the API key exists in the request headers
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                // If not, return 401 Unauthorized
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key was not provided.");
                return; // Short-circuit the pipeline
            }

            // Validate the extracted API key against the one in configuration
            if (!string.Equals(extractedApiKey, _apiKey))
            {
                // If invalid, return 401 Unauthorized
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized client.");
                return; // Short-circuit the pipeline
            }

            // If valid, proceed to the next middleware in the pipeline
            await _next(context);
        }
    }
}

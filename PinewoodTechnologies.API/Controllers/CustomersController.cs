using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinewoodTechnologies.API.Data;
using PinewoodTechnologies.API.Models;

namespace PinewoodTechnologies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route: api/customers
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        // Constructor to inject the database context
        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            // Retrieve all customers from the database
            return await _context.Customers.ToListAsync();
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            // Find the customer with the specified ID
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                // Return 404 Not Found if the customer doesn't exist
                return NotFound();
            }

            // Return the customer
            return customer;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            // Add the new customer to the context
            _context.Customers.Add(customer);
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return 201 Created with the location of the new customer
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            // Ensure the ID in the URL matches the ID in the body
            if (id != customer.Id)
            {
                // Return 400 Bad Request if IDs do not match
                return BadRequest();
            }

            // Mark the customer entity as modified
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle the case where the customer doesn't exist anymore
                if (!_context.Customers.Any(e => e.Id == id))
                {
                    // Return 404 Not Found
                    return NotFound();
                }
                else
                {
                    // Re-throw if it's another concurrency exception
                    throw;
                }
            }

            // Return 204 No Content to indicate success
            return NoContent();
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            // Find the customer to delete
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                // Return 404 Not Found if the customer doesn't exist
                return NotFound();
            }

            // Remove the customer from the context
            _context.Customers.Remove(customer);
            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return 204 No Content to indicate success
            return NoContent();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PinewoodTechnologies.API.Models;

namespace PinewoodTechnologies.API.Data
{
    // DbContext for interacting with the database
    public class CustomerContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        // DbSet representing the Customers table
        public DbSet<Customer> Customers { get; set; }
    }
}

using CustomerProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerProject.Models.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        // to change connection string?
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // add entity relationships here?
            // maybe even seed with .HasData
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = new Guid("488AB5B8-FEBB-43AD-A136-2DD9BC5A0264"), FirstName = "John", LastName = "Smith", EmailAddress = "johnsmith@email.com" },
                new Customer { Id = new Guid("E68DD37A-03B6-4952-9EDA-D62E02D4B4D9"), FirstName = "John", LastName = "Doe", EmailAddress = "johndoe@email.com" },
                new Customer { Id = new Guid("E59DA49C-B5A9-4A2C-83F2-064A0A43D9A9"), FirstName = "Rose", LastName = "Tyler", EmailAddress = "rosety@email.com" }
            );
        }
    }
}

using CustomerProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerProject.Models.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(Guid id)
        {
            if (id == Guid.Empty) 
            {
                return null;
            }

            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Customer customer) 
        {
            _context.Update(customer);
            _context.SaveChanges();
        }

        public void Add(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _context.Remove(customer);
            _context.SaveChanges();
        }

        public bool CheckForDuplicate(string emailAddress, Guid? id = null)
        {
            return _context.Customers.Any(x => (id != null && x.Id != id) && x.EmailAddress.Equals(emailAddress));
        }
    }
}

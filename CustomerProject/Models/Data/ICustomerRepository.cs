using CustomerProject.Models.Domain;

namespace CustomerProject.Models.Data
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer GetById(Guid id);

        void Update(Customer customer);
        
        void Add(Customer customer);
        
        void Delete(Customer customer);

        bool CheckForDuplicate(string emailAddress, Guid? id = null);
    }
}

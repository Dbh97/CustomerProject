using CustomerProject.Models;
using CustomerProject.Models.Domain;
using CustomerProject.Models.ViewModels;

namespace CustomerProject.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();

        CustomerServiceResult GetById(Guid id);

        CustomerServiceResult Update(EditCustomerViewModel customer);

        CustomerServiceResult Add(AddCustomerViewModel vm);

        CustomerServiceResult Delete(Guid id);
    }
}

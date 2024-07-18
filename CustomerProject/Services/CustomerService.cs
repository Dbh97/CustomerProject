using CustomerProject.Models;
using CustomerProject.Models.Data;
using CustomerProject.Models.Domain;
using CustomerProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CustomerProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerServiceResult GetById(Guid id)
        {
            var serviceResult = new CustomerServiceResult();

            if (id == Guid.Empty) 
            {
                serviceResult.ServiceErrorType = ServiceErrorType.BadRequest;
                serviceResult.ErrorMessage = "Id is not valid";

                return serviceResult;

            }

            var customer = _customerRepository.GetById(id);

            serviceResult.Entity = customer;

            return serviceResult;

        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();

            return customers;
        }

        public CustomerServiceResult Update(EditCustomerViewModel editViewModel)
        {
            var serviceResult = new CustomerServiceResult();

            var customer = _customerRepository.GetById(editViewModel.Id);

            if (customer == null)
            {
                serviceResult.ServiceErrorType = ServiceErrorType.NotFound;
                serviceResult.ErrorMessage = "Customer not found";

                return serviceResult;
            }

            var duplicateEmail = _customerRepository.CheckForDuplicate(editViewModel.EmailAddress, editViewModel.Id);

            if (duplicateEmail)
            {
                serviceResult.ServiceErrorType = ServiceErrorType.BadRequest;
                serviceResult.ErrorMessage = "Customer exists for chosen email address";

                return serviceResult;
            }

            customer.FirstName = editViewModel.FirstName;
            customer.LastName = editViewModel.LastName;
            customer.EmailAddress = editViewModel.EmailAddress;

            serviceResult.Entity = customer;

            _customerRepository.Update(customer);

            return serviceResult;
        }

        public CustomerServiceResult Add(AddCustomerViewModel addViewModel)
        {
            var serviceResult = new CustomerServiceResult();

            var duplicateEmail = _customerRepository.CheckForDuplicate(addViewModel.EmailAddress);

            if (duplicateEmail)
            {
                serviceResult.ServiceErrorType = ServiceErrorType.BadRequest;
                serviceResult.ErrorMessage = "Customer exists for chosen email address";

                return serviceResult;
            }

            var customer = new Customer
            {
                FirstName = addViewModel.FirstName,
                LastName = addViewModel.LastName,
                EmailAddress = addViewModel.EmailAddress,
            };

            _customerRepository.Add(customer);

            serviceResult.Entity = customer;

            return serviceResult;
        }

        public CustomerServiceResult Delete(Guid id)
        {
            var serviceResult = new CustomerServiceResult();


            if (id == Guid.Empty)
            {
                serviceResult.ServiceErrorType = ServiceErrorType.BadRequest;
                serviceResult.ErrorMessage = "Id is not valid";

                return serviceResult;

            }

            var customer = _customerRepository.GetById(id);

            if (customer == null) 
            {
                serviceResult.ServiceErrorType = ServiceErrorType.NotFound;
                serviceResult.ErrorMessage = "Customer to be deleted not found";

                return serviceResult;
            }

            _customerRepository.Delete(customer);

            serviceResult.Entity = customer;

            return serviceResult;
        }
    }
}

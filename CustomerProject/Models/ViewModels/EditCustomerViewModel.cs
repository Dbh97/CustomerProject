using CustomerProject.Models.Domain;
using CustomerProject.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace CustomerProject.Models.ViewModels
{
    public class EditCustomerViewModel : AddCustomerViewModel
    {
        public Guid Id { get; set; }

        public void ConvertFrom(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            EmailAddress = customer.EmailAddress;
        }
    }
}

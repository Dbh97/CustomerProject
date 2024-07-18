using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerProject.Models.ViewModels
{
    public class AddCustomerViewModel
    {
        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}

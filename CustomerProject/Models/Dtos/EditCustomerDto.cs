using System.ComponentModel.DataAnnotations;

namespace CustomerProject.Models.Dtos
{
    public class EditCustomerDto
    {
        public Guid Id;

        [Required]
        [StringLength(50)]
        public string FirstName;

        [Required]
        [StringLength(50)]
        public string LastName;

        [Required]
        [StringLength(50)]
        public string EmailAddress;
    }
}

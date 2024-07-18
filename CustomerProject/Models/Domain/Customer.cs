using System.ComponentModel.DataAnnotations;

namespace CustomerProject.Models.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }
    }
}

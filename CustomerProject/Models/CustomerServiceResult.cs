using CustomerProject.Models.Domain;

namespace CustomerProject.Models
{
    public class CustomerServiceResult
    {
        public Customer? Entity { get; set; } = null;

        public ServiceErrorType? ServiceErrorType { get; set; } = null;

        public string? ErrorMessage { get; set; } = null;
    }
}

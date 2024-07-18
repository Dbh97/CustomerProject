using CustomerProject.Models;
using CustomerProject.Models.Data;
using CustomerProject.Models.Domain;
using CustomerProject.Models.ViewModels;
using CustomerProject.Services;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CustomerProject.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;

        private readonly Mock<ICustomerRepository> _repo;

        private readonly Customer _customer;
        private readonly Guid _customerId;

        public CustomerServiceTests()
        {
            _repo = new Mock<ICustomerRepository>();
            _customerId = new Guid("E68DD37A-03B6-4952-9EDA-D62E02D4B4D9");
            _customer = new Customer { Id = _customerId, FirstName = "John", LastName = "Doe", EmailAddress = "johndoe@email.com" };

            _sut = new CustomerService(_repo.Object);
        }

        [Test]
        public void GetById_ReturnsIdIsNotValid_WhenIdIsEmpty()
        {
            var id = Guid.Empty;

            var result = _sut.GetById(id);

            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ServiceErrorType, Is.Not.Null);
            Assert.That(result.Entity, Is.Null);
            Assert.That(result.ServiceErrorType, Is.EqualTo(ServiceErrorType.BadRequest));
            Assert.That(result.ErrorMessage, Is.EqualTo("Id is not valid"));
        }

        [Test]
        public void GetById_ReturnsNullCustomer_IfCustomerNotFound()
        {
            var id = Guid.NewGuid();
            _repo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Customer?)null);

            var result = _sut.GetById(id);

            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.ServiceErrorType, Is.Null);
            Assert.That(result.Entity, Is.Null);
        }

        [Test]
        public void GetById_ReturnsCustomer_IfCustomerExists()
        {
            _repo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_customer);

            var result = _sut.GetById(_customerId);

            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.ServiceErrorType, Is.Null);
            Assert.That(result.Entity, Is.Not.Null);
            Assert.That(result.Entity.Id, Is.EqualTo(_customerId));
        }

        [Test]
        public void Update_ReturnsCustomerNotFound_IfCustomerDoesntExist()
        {
            var viewModel = CreateEditViewModel();
            _repo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Customer?)null);

            var result = _sut.Update(viewModel);

            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ServiceErrorType, Is.Not.Null);
            Assert.That(result.Entity, Is.Null);
            Assert.That(result.ServiceErrorType, Is.EqualTo(ServiceErrorType.NotFound));
            Assert.That(result.ErrorMessage, Is.EqualTo("Customer not found"));
        }

        [Test]
        public void Update_ReturnsCustomerExistsForEmail_IfDuplicateEmail()
        {
            var viewModel = CreateEditViewModel(_customerId);
            _repo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_customer);
            _repo.Setup(x => x.CheckForDuplicate(It.IsAny<string>(), It.IsAny<Guid>())).Returns(true);

            var result = _sut.Update(viewModel);

            Assert.That(result.ErrorMessage, Is.Not.Null);
            Assert.That(result.ServiceErrorType, Is.Not.Null);
            Assert.That(result.Entity, Is.Null);
            Assert.That(result.ServiceErrorType, Is.EqualTo(ServiceErrorType.BadRequest));
            Assert.That(result.ErrorMessage, Is.EqualTo("Customer exists for chosen email address"));
        }

        [Test]
        public void Update_ReturnsEditedCustomer_IfCustomerExistsAndNoDuplicateEmail()
        {
            var viewModel = CreateEditViewModel(_customerId);
            _repo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_customer);
            _repo.Setup(x => x.CheckForDuplicate(It.IsAny<string>(), It.IsAny<Guid>())).Returns(false);

            var result = _sut.Update(viewModel);

            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.ServiceErrorType, Is.Null);
            Assert.That(result.Entity, Is.Not.Null);
            Assert.That(result.Entity.Id, Is.EqualTo(_customerId));
            Assert.That(result.Entity.FirstName, Is.EqualTo("EditedFirstName"));
        }


        private EditCustomerViewModel CreateEditViewModel(Guid? id = null)
        {
            return new EditCustomerViewModel
            {
                Id = id == null ? Guid.NewGuid() : id.Value,
                FirstName = "EditedFirstName",
                LastName = "EditedLastName",
                EmailAddress = "EditedEmailAddress"
            };
        }
    }
}

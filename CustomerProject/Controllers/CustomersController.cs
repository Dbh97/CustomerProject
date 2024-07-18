using CustomerProject.Models;
using CustomerProject.Models.ViewModels;
using CustomerProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = _customerService.GetCustomers();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = _customerService.Add(vm);

                if (result.ErrorMessage != null)
                {
                    return ServiceResponse(result);
                }

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ViewBag.Action = "Edit";

            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = _customerService.GetById(id);

            if (result.ErrorMessage != null)
            {
                return ServiceResponse(result);
            }

            if (result.Entity == null)
            {
                return NotFound();
            }

            var viewModel = new EditCustomerViewModel();

            viewModel.ConvertFrom(result.Entity);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = _customerService.Update(vm);

                if (result.ErrorMessage != null)
                {
                    return ServiceResponse(result);
                }

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _customerService.Delete(id);

            if (result.ErrorMessage != null)
            {
                return ServiceResponse(result);
            }

            return RedirectToAction("Index");
        }

        private IActionResult ServiceResponse(CustomerServiceResult result)
        {
            if (result.ServiceErrorType.GetValueOrDefault() == ServiceErrorType.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}

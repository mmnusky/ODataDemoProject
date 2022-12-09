using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODataDemoProject.Models.Customers.Exceptions;
using ODataDemoProject.Models;
using ODataDemoProject.Services.Foundations;
using RESTFulSense.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace ODataDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : RESTFulController
    {

        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            try
            {
                Customer createdTenant =
                    await this.customerService.CreateCustomerAsync(customer);

                return Created(createdTenant);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is AlreadyExistsCustomerException)
            {
                return Conflict(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Customer>> GetAllCustomers()
        {
            try
            {
                IQueryable<Customer> storageTenants =
                    this.customerService.RetrieveAllCustomer();

                return Ok(storageTenants);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return Problem(customerServiceException.Message);
            }
        }
        [HttpGet("count")]
        public ActionResult GetAllCustomersCount()
        {
            try
            {
                var storageTenantsCount =
                     this.customerService.RetrieveAllCustomer().Count();

                return Ok(storageTenantsCount);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return Problem(customerServiceException.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerAsync(Guid customerId)
        {
            try
            {
                Customer storageTenant =
                    await this.customerService.RetreiveCustomerByIdAsync(customerId);

                return Ok(storageTenant);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return BadRequest(customerValidationException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return Problem(customerServiceException.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer customer)
        {
            try
            {
                Customer registeredTenant =
                    await this.customerService.ModifyCustomerAsync(customer);

                return Ok(registeredTenant);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return BadRequest(innerMessage);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return Problem(customerServiceException.Message);
            }
        }

        [HttpDelete("{customerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteCustomerAsync(Guid customerId)
        {
            try
            {
                Customer storageTenant =
                    await this.customerService.RemoveCustomerByIdAsync(customerId);

                return Ok(storageTenant);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);

                return BadRequest(customerValidationException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return Problem(customerServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
            exception.InnerException.Message;

    }
}

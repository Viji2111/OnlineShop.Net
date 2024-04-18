using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using LazyCache;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PresentationLayer.Filters;
using Services.Interfaces;

namespace Project1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerservice)
        {
            _customerService = customerservice;
        }

        [HttpGet]
        [Route("/GetCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            return Ok(customers);
        }
        [HttpGet]
        [Route("/GetCustomerCache")]
        public async Task<ActionResult> GetCustomerCache()
        {

            var customers = _customerService.GetCustomerCache() ;
             return Ok(customers);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var addedCustomer = await _customerService.PostCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = addedCustomer.CustomerId }, addedCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id,Customer customer)
        {
            var result = await _customerService.PutCustomer(id,customer);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
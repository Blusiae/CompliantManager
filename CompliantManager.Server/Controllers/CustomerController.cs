using CompliantManager.Server.Extensions;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompliantManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] int count, [FromQuery] int offset)
        {
            var customers = await _customerService.GetAll(offset, count);

            if (customers.Count == 0)
                return NotFound("No customers found.");

            var customerDtos = customers.Select(u => u.ToDto()).ToList();

            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetById(id);
            if (customer is null)
                return NotFound("Customer not found.");

            var customerDto = customer.ToDto();
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            var customer = customerDto.ToEntity();
            await _customerService.Create(customer);

            return Ok(customer.Id);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCustomersCount()
        {
            return Ok(await _customerService.GetCount());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customerDto)
        {
            await _customerService.Edit(customerDto.ToEntity());

            return Ok("User updated successfully.");
        }

        [HttpPost("deleteMultiple")]
        public async Task<IActionResult> DeleteCustomers([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("No customer IDs provided.");

            foreach (var id in ids)
            {
                await _customerService.Delete(id);
            }

            return Ok("Users deleted successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.Delete(id);
            return result ? Ok("User deleted successfully.") : BadRequest();
        }
    }
}

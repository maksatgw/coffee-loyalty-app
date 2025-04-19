using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.DTOs.CustomerDtos;
using CoffeLoyaltyApp.Repositories.CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace CoffeeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        public CustomersController(ICustomerRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dtos = list.Select(c => new CustomerDto(c.CustomerId, c.Name, c.Email, c.Phone, c.QRCode));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(Guid id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(new CustomerDto(c.CustomerId, c.Name, c.Email, c.Phone, c.QRCode));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var entity = new Customer { Name = dto.Name, Email = dto.Email, Phone = dto.Phone };
            var created = await _repo.CreateAsync(entity);
            var result = new CustomerDto(created.CustomerId, created.Name, created.Email, created.Phone, created.QRCode);
            return CreatedAtAction(nameof(Get), new { id = created.CustomerId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto dto)
        {
            if (id != dto.CustomerId) return BadRequest();
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            await _repo.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}

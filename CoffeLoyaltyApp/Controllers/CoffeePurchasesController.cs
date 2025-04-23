using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.DTOs.CoffeePurchaseDtos;
using CoffeLoyaltyApp.Repositories.CoffeePurchaseRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeePurchasesController : ControllerBase
    {
        private readonly ICoffeePurchaseRepository _repo;
        public CoffeePurchasesController(ICoffeePurchaseRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDetailDto>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dtos = list.Select(p => new PurchaseDetailDto(p.PurchaseId, p.CustomerId, p.MenuItemId, p.PurchaseDate, p.IsFree));
            return Ok(dtos);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<PurchaseDetailDto>>> GetByCustomer(Guid customerId)
        {
            var list = await _repo.GetByCustomerAsync(customerId);
            var dtos = list.Select(p => new PurchaseDetailDto(p.PurchaseId, p.CustomerId, p.MenuItemId, p.PurchaseDate, p.IsFree));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDetailDto>> Create([FromBody] PurchaseDto dto)
        {
            var entity = new CoffeePurchase { CustomerId = dto.CustomerId, MenuItemId = dto.MenuItemId };
            // Free logic should be handled inside repository or a service before saving
            var created = await _repo.CreateAsync(entity);
            var result = new PurchaseDetailDto(created.PurchaseId, created.CustomerId, created.MenuItemId, created.PurchaseDate, created.IsFree);
            return CreatedAtAction(nameof(GetAll), new { id = created.PurchaseId }, result);
        }
        [Authorize(Roles = "Admin,Barista")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}

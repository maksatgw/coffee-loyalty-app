using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.DTOs.MenuItemDtos;
using CoffeLoyaltyApp.Repositories.MenuItemRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemRepository _repo;
        public MenuItemsController(IMenuItemRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dtos = list.Select(m => new MenuItemDto(m.MenuItemId, m.Name, m.Description, m.Price));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> Get(Guid id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null) return NotFound();
            return Ok(new MenuItemDto(m.MenuItemId, m.Name, m.Description, m.Price));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create([FromBody] CreateMenuItemDto dto)
        {
            var entity = new MenuItem { Name = dto.Name, Description = dto.Description, Price = dto.Price };
            var created = await _repo.CreateAsync(entity);
            var result = new MenuItemDto(created.MenuItemId, created.Name, created.Description, created.Price);
            return CreatedAtAction(nameof(Get), new { id = created.MenuItemId }, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMenuItemDto dto)
        {
            if (id != dto.MenuItemId) return BadRequest();
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            await _repo.UpdateAsync(existing);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}

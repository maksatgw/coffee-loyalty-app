using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/menuitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        // GET: api/menuitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(Guid id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/menuitems
        [HttpPost]
        public async Task<ActionResult<MenuItem>> CreateMenuItem(MenuItem item)
        {
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenuItem), new { id = item.MenuItemId }, item);
        }

        // PUT: api/menuitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(Guid id, MenuItem item)
        {
            if (id != item.MenuItemId)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MenuItems.Any(m => m.MenuItemId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/menuitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

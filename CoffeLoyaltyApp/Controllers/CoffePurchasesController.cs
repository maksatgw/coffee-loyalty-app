using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.DTOs;
using CoffeeLoyaltyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeePurchasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoffeePurchasesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/coffeepurchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeePurchase>>> GetAllPurchases()
        {
            return await _context.CoffeePurchases
                .Include(p => p.Customer)
                .Include(p => p.MenuItem)
                .ToListAsync();
        }

        // GET: api/coffeepurchases/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CoffeePurchase>>> GetPurchasesByCustomer(Guid customerId)
        {
            var purchases = await _context.CoffeePurchases
                .Where(p => p.CustomerId == customerId)
                .Include(p => p.MenuItem)
                .ToListAsync();

            return purchases;
        }

        [HttpPost]
        public async Task<ActionResult<CoffeePurchase>> AddPurchase([FromBody] PurchaseDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // Yeni CoffeePurchase objesini elle oluşturuyoruz
            var purchase = new CoffeePurchase
            {
                CustomerId = dto.CustomerId,
                MenuItemId = dto.MenuItemId,
                // PurchaseDate ve PurchaseId zaten default atılıyor
            };

            // 1. Son bedava kahveyi al
            var lastFree = await _context.CoffeePurchases
                .Where(p => p.CustomerId == purchase.CustomerId && p.IsFree)
                .OrderByDescending(p => p.PurchaseDate)
                .FirstOrDefaultAsync();

            var threshold = lastFree?.PurchaseDate ?? DateTime.MinValue;

            // 2. O tarihten sonraki non-free kahveleri say
            var countSinceLastFree = await _context.CoffeePurchases
                .Where(p =>
                    p.CustomerId == purchase.CustomerId &&
                    !p.IsFree &&
                    p.PurchaseDate > threshold)
                .CountAsync();

            // 3. 6 kahveden sonra (7. sipariş) bedava
            if (countSinceLastFree + 1 == 7)
                purchase.IsFree = true;

            _context.CoffeePurchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllPurchases),
                new { id = purchase.PurchaseId }, purchase);
        }



        // DELETE: api/coffeepurchases/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            var purchase = await _context.CoffeePurchases.FindAsync(id);
            if (purchase == null)
                return NotFound();

            _context.CoffeePurchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.Repositories.CustomerRepository;
using Microsoft.EntityFrameworkCore;

namespace CoffeLoyaltyApp.Repositories.CoffeePurchaseRepository
{
    public class CoffeePurchaseRepository : ICoffeePurchaseRepository
    {
        private readonly AppDbContext _ctx;
        public CoffeePurchaseRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<CoffeePurchase>> GetAllAsync() =>
            await _ctx.CoffeePurchases.Include(p => p.MenuItem).Include(p => p.Customer).ToListAsync();

        public async Task<IEnumerable<CoffeePurchase>> GetByCustomerAsync(Guid customerId)
        {
            return await _ctx.CoffeePurchases
                .Where(p => p.CustomerId == customerId)
                .Include(p => p.MenuItem)
                .Include(p => p.Customer)
                .ToListAsync();
        }

        public async Task<CoffeePurchase> CreateAsync(CoffeePurchase entity)
        {
            // 1. Find last free purchase date
            var lastFree = await _ctx.CoffeePurchases
                .Where(p => p.CustomerId == entity.CustomerId && p.IsFree)
                .OrderByDescending(p => p.PurchaseDate)
                .FirstOrDefaultAsync();

            var threshold = lastFree?.PurchaseDate ?? DateTime.MinValue;

            // 2. Count non-free since threshold
            var count = await _ctx.CoffeePurchases
                .Where(p => p.CustomerId == entity.CustomerId && !p.IsFree && p.PurchaseDate > threshold)
                .CountAsync();

            // 3. If next is 7th, mark free
            if (count + 1 == 7)
                entity.IsFree = true;

            // 4. Add and save
            _ctx.CoffeePurchases.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var p = await _ctx.CoffeePurchases.FindAsync(id);
            if (p != null) { _ctx.CoffeePurchases.Remove(p); await _ctx.SaveChangesAsync(); }
        }
    }
}

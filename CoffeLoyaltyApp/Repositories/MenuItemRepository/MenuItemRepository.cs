using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeLoyaltyApp.Repositories.MenuItemRepository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _ctx;
        public MenuItemRepository(AppDbContext ctx) => _ctx = ctx;
        public async Task<IEnumerable<MenuItem>> GetAllAsync() => await _ctx.MenuItems.ToListAsync();
        public async Task<MenuItem?> GetByIdAsync(Guid id) => await _ctx.MenuItems.FindAsync(id);
        public async Task<MenuItem> CreateAsync(MenuItem entity)
        {
            _ctx.MenuItems.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(MenuItem entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var m = await _ctx.MenuItems.FindAsync(id);
            if (m is not null)
            {
                _ctx.MenuItems.Remove(m);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}

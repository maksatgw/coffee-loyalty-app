using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeLoyaltyApp.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _ctx;
        public CustomerRepository(AppDbContext ctx) => _ctx = ctx;
        public async Task<IEnumerable<Customer>> GetAllAsync() => await _ctx.Customers.ToListAsync();
        public async Task<Customer?> GetByIdAsync(Guid id) => await _ctx.Customers.FindAsync(id);
        public async Task<Customer> CreateAsync(Customer entity)
        {
            _ctx.Customers.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Customer entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var c = await _ctx.Customers.FindAsync(id);
            if (c is not null)
            {
                _ctx.Customers.Remove(c);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}

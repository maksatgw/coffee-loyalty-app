using CoffeeLoyaltyApp.Models;

namespace CoffeLoyaltyApp.Repositories.MenuItemRepository
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(Guid id);
        Task<MenuItem> CreateAsync(MenuItem entity);
        Task UpdateAsync(MenuItem entity);
        Task DeleteAsync(Guid id);
    }
}

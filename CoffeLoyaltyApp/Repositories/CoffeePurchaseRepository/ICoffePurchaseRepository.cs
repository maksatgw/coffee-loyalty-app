using CoffeeLoyaltyApp.Models;

namespace CoffeLoyaltyApp.Repositories.CoffeePurchaseRepository
{
    public interface ICoffeePurchaseRepository
    {
        Task<IEnumerable<CoffeePurchase>> GetAllAsync();
        Task<IEnumerable<CoffeePurchase>> GetByCustomerAsync(Guid customerId);
        Task<CoffeePurchase> CreateAsync(CoffeePurchase entity);
        Task DeleteAsync(Guid id);
    }
}

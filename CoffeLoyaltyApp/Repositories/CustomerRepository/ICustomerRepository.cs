using CoffeeLoyaltyApp.Models;

namespace CoffeLoyaltyApp.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Guid id);
    }
}

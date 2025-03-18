

using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
    }
}

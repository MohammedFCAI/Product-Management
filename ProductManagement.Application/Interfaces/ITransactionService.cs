using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task CreateTransactionAsync2(Transaction transaction);
        Task<string> CreateTransactionAsync(Transaction transaction);
    }
}

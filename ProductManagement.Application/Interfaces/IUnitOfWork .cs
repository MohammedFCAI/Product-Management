using Microsoft.EntityFrameworkCore.Storage;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ITransactionRepository Transactions { get; }
        Task<int> CompleteAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}

using ProductManagement.Application.Interfaces;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; }
        public ITransactionRepository Transactions { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, ITransactionRepository transactionRepository)

        {
            _context = context;
            Products = productRepository;
            Transactions = transactionRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

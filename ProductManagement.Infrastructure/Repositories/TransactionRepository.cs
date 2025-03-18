using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;


namespace ProductManagement.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.Include(t => t.Product).ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id) => await _context.Transactions.Include(t => t.Product).FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}

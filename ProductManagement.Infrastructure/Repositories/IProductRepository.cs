using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        void Update(Product product);
        Task DeleteAsync(int id);
    }
}

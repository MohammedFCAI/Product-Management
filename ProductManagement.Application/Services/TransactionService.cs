using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _unitOfWork.Transactions.GetAllAsync();
        }



        public async Task<string> CreateTransactionAsync(Transaction transaction)
        {
            using var transactionScope = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(transaction.ProductId);
                if (product == null)
                {
                    return "Product not found.";
                }

                if (product.InitialQuantity < transaction.Quantity)
                {
                    return $"Only {product.InitialQuantity} quantity available for {product.Name}.";
                }

                product.InitialQuantity -= transaction.Quantity;
                transaction.TotalPrice = product.Price * transaction.Quantity;

                await _unitOfWork.Transactions.AddAsync(transaction);
                _unitOfWork.Products.Update(product);

                await _unitOfWork.CompleteAsync();
                await transactionScope.CommitAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                await transactionScope.RollbackAsync();
                return $"Transaction failed: {ex.Message}";
            }
        }

    }
}

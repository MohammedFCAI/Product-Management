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

        public async Task CreateTransactionAsyncV1(Transaction transaction)
        {
            transaction.TotalPrice = transaction.Product.Price * transaction.Quantity;
            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CreateTransactionAsync2(Transaction transaction)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(transaction.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (product.InitialQuantity < transaction.Quantity)
            {
                throw new Exception("Not enough stock available.");
            }

            product.InitialQuantity -= transaction.Quantity;
            transaction.TotalPrice = product.Price * transaction.Quantity;

            await _unitOfWork.Transactions.AddAsync(transaction);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string> CreateTransactionAsync(Transaction transaction)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(transaction.ProductId);
            if (product == null)
            {
                return "Product not found.";
            }

            if (product.InitialQuantity < transaction.Quantity)
            {
                return $"Only {product.InitialQuantity} unit(s) available for {product.Name}.";
            }

            product.InitialQuantity -= transaction.Quantity;
            transaction.TotalPrice = product.Price * transaction.Quantity;

            await _unitOfWork.Transactions.AddAsync(transaction);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();

            return "Success";
        }


    }
}

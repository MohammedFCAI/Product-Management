using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.ViewModels;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly IToastNotification _toastNotification;
        public TransactionController(ITransactionService transactionService, IProductService productService, IToastNotification toastNotification)
        {
            _transactionService = transactionService;
            _productService = productService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }


        public async Task<IActionResult> Create()
        {
            var products = await _productService.GetAllProductsAsync();
            ViewBag.Products = products;
            return View(new TransactionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetProductByIdAsync(transaction.ProductId);
                if (product == null)
                {
                    ModelState.AddModelError("", "Selected product does not exist.");
                    ViewBag.Products = await _productService.GetAllProductsAsync();
                    return View(transaction);
                }

                transaction.Unit = product.Unit;
                transaction.Price = product.Price;
                transaction.TotalPrice = transaction.Price * transaction.Quantity;

                var newTransaction = new Transaction
                {
                    ProductId = transaction.ProductId,
                    Quantity = transaction.Quantity,
                    Date = transaction.Date,
                    TotalPrice = transaction.TotalPrice
                };

                var result = await _transactionService.CreateTransactionAsync(newTransaction);
                if (result == "Success")
                    _toastNotification.AddSuccessToastMessage("Transaction Created Successfully!");
                else
                {

                    _toastNotification.AddErrorToastMessage(result);
                    ViewBag.Products = await _productService.GetAllProductsAsync();
                    return View(transaction);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Products = await _productService.GetAllProductsAsync();
            return View(transaction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(transaction);
            }

            var product = await _productService.GetProductByIdAsync(transaction.ProductId);
            if (product == null)
            {
                _toastNotification.AddErrorToastMessage("Selected product does not exist.");
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(transaction);
            }

            // Assign product details to the transaction
            transaction.Unit = product.Unit;
            transaction.Price = product.Price;
            transaction.TotalPrice = transaction.Price * transaction.Quantity;

            // Create a new transaction entity
            var newTransaction = new Transaction
            {
                ProductId = transaction.ProductId,
                Quantity = transaction.Quantity,
                Date = transaction.Date,
                TotalPrice = transaction.TotalPrice
            };

            // Process transaction creation
            var result = await _transactionService.CreateTransactionAsync(newTransaction);
            if (result != "Success")
            {
                _toastNotification.AddErrorToastMessage(result);
                ViewBag.Products = await _productService.GetAllProductsAsync();
                return View(transaction);
            }

            _toastNotification.AddSuccessToastMessage("Transaction Created Successfully!");
            return RedirectToAction("Index");
        }


    }
}

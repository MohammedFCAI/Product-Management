using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productService, IToastNotification toastNotification)
        {
            _productService = productService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index() => View(await _productService.GetAllProductsAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                _toastNotification.AddSuccessToastMessage("Product Created Successfully!");

                return RedirectToAction("Index");
            }
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                _toastNotification.AddSuccessToastMessage("Product Edited Successfully!");
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            _toastNotification.AddSuccessToastMessage("Product Deleted Successfully!");

            return RedirectToAction("Index");
        }
    }
}

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

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _productService.GetAllProductsAsync());



        [HttpGet]
        public async Task<IActionResult> GetProducts2()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (products == null || !products.Any())
                    return Json(new { data = new List<Product>() });
                return Json(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching products" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts3()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (products == null) products = new List<Product>();

                return Json(new { data = products });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message }); // Return actual error for debugging
            }
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }

                await _productService.DeleteProductAsync(id);
                _toastNotification.AddSuccessToastMessage("Product Deleted Successfully!");
                return Json(new { success = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the product" });
            }
        }
    }
}

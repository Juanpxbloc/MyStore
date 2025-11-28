using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;

namespace MyStore.Controllers
{
    public class ProductController(ProductService _productService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync(); //Returns all products
            return View(products); 
        }

        [HttpGet] 
        public async Task<IActionResult> AddEdit(int id) 
        {
            var productVM = await _productService.GetByIdAsync(id); 
            return View(productVM);
        }

        [HttpPost] 
        public async Task<IActionResult> AddEdit(ProductVM entityVM)
        {
            ViewBag.message = null;
            ModelState.Remove("Categories");
            ModelState.Remove("Category.Name");
            if (!ModelState.IsValid) return View(entityVM); 

            if (entityVM.ProductId == 0)
            {
                await _productService.AddAsync(entityVM); 
                ModelState.Clear(); 
                entityVM = new ProductVM(); 
                ViewBag.message = "Product Created"; 
            }
            else
            {
                await _productService.EditAsync(entityVM); 
                ViewBag.message = " Product Edited!"; 
            }
            return View(entityVM); 
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id); 
            return RedirectToAction("Index"); 
        }
    }
}
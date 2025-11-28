using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;

/*using Microsoft.AspNetCore.Mvc;

namespace MyStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}*/ // Controller default structure for ASP.NET MVC Controllers

namespace MyStore.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller //Get the database context from category service
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync(); // await bc its async
            return View(categories); // Pass the categories to the view
        }

        [HttpGet] // Get is set by deafult, but we can specify it for clarity
        public async Task<IActionResult> AddEdit(int id) //This is the method that returns the view
        {
            var categoryVM = await _categoryService.GetByIdAsync(id); // Create a new instance of CategoryVM
            return View(categoryVM);
        }

        [HttpPost] // Get is set by deafult, post must be declared. 
        public async Task<IActionResult> AddEdit(CategoryVM entityVM)
        {
            ViewBag.message = null; // Clear any previous messages

            if (!ModelState.IsValid) return View(entityVM); // If not, return the view with the model to show validation errors

            if (entityVM.CategoryId == 0)
            {
                await _categoryService.AddAsync(entityVM); // Call the service to add the new category
                ModelState.Clear(); // Clear the model state to reset the form
                entityVM = new CategoryVM(); // Create a new instance of CategoryVM to clear the form
                ViewBag.message = "Category added successfully"; // Message to show in the view
            }
            else
            {
                await _categoryService.EditAsync(entityVM); // Call the service to edit the category
                ViewBag.message = "Edited!"; // Message to show in the view
            }
            return View(entityVM); // Return the view with the model
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id); // Call the service to delete the category
            return RedirectToAction("Index"); // Redirect to the Index action to show the updated list
        }
        
    }
}



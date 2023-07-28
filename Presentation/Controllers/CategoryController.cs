using Business.Services.Abstract.User;
using Business.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult List()
        {
            return View();
        }

        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var isSucceded = await  _categoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(List));
            
            return View(model); 
        }
    }
}

using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.FAQCategory;
using Business.ViewModels.Admin.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _productCategoryService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryCreateVM model)
        {
            var isSucceded = await _productCategoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var newmodel = await _productCategoryService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductCategoryUpdateVM model)
        {
            var isSucceded = await _productCategoryService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productCategoryService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }
    }
}

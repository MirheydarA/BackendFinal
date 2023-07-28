using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Product;
using Business.ViewModels.Admin.Question;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.IndexGetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCreateAsync();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var isSucceded = await _productService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var newmodel = await _productService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM model)
        {
            var isSucceded = await _productService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }
    }
}

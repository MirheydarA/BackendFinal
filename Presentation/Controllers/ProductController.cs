using Business.Services.Abstract.User;
using Business.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(ProductIndexVM model) 
        {
            return View(await _productService.IndexGetAllAsync(model));
        }
    }
}

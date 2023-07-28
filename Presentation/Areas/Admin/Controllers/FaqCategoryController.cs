using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Duty;
using Business.ViewModels.Admin.FAQCategory;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqCategoryController : Controller
    {
        private readonly IFAQCategoryService _FAQCategoryService;

        public FaqCategoryController(IFAQCategoryService FAQCategoryService)
        {
            _FAQCategoryService = FAQCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _FAQCategoryService.GetAllAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCategoryCreateVM model)
        {
            var isSucceded = await _FAQCategoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
           
            var newmodel = await _FAQCategoryService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel); 
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, FAQCategoryUpdateVM model)
        {
            var isSucceded = await _FAQCategoryService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _FAQCategoryService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }


    }
}

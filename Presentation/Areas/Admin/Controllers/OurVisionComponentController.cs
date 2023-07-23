using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.OurVisionComponent;
using Business.ViewModels.Admin.Slider;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OurVisionComponentController : Controller
    {
        private readonly IOurVisionComponentService _ourVisionComponentService;
        private readonly IOurVisionComponentRepository _ourVisionComponentRepository;

        public OurVisionComponentController(IOurVisionComponentService ourVisionComponentService, IOurVisionComponentRepository ourVisionComponentRepository)
        {
            _ourVisionComponentService = ourVisionComponentService;
            _ourVisionComponentRepository = ourVisionComponentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new OurVisionComponentIndexVM()
            {
                OurVisionComponents = await _ourVisionComponentRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurVisionComponentCreateVM model)
        {
            var isSucceded = await _ourVisionComponentService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var ourVisionComponent = await _ourVisionComponentRepository.GetByIdAsync(id);
            if (ourVisionComponent == null) return NotFound("Komponent tapilmadi");

            var model = new OurVisionComponentUpdateVM()
            {
                Title = ourVisionComponent.Title,
                Description = ourVisionComponent.Description,
                //Photoname = ourVisionComponent.Photoname
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OurVisionComponentUpdateVM model)
        {
            var isSucceded = await _ourVisionComponentService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _ourVisionComponentService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();
        }




    }
}

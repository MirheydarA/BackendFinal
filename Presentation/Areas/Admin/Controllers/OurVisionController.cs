using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.OurVision;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OurVisionController : Controller
    {
        private readonly IOurVisionService _ourVisionService;
        private readonly IOurVisionRepository _ourVisionRepository;

        public OurVisionController(IOurVisionService ourVisionService, IOurVisionRepository ourVisionRepository)
        {
            _ourVisionService = ourVisionService;
            _ourVisionRepository = ourVisionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurVisionCreateVM model)
        {
            var isSucceded = await _ourVisionService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }
    }
}

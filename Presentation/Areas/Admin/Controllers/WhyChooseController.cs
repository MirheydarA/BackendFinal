using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Admin.WhyChoose;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhyChooseController : Controller
    {
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IWhyChooseService _whyChooseService;

        public WhyChooseController(IWhyChooseRepository whyChooseRepository, IWhyChooseService whyChooseService)
        {
            _whyChooseRepository = whyChooseRepository;
            _whyChooseService = whyChooseService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new WhyChooseIndexVM()
            {
                WhyChooses = await _whyChooseRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhyChooseCreateVM model)
        {
            var isSucceded = await _whyChooseService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var whyChoose = await _whyChooseRepository.GetByIdAsync(id);
            if (whyChoose == null) return NotFound("Whychoose tapilmadi");

            var model = new WhyChooseUpdateVM()
            {
                Description = whyChoose.Description,

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WhyChooseUpdateVM model)
        {
            var isSucceded = await _whyChooseService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _whyChooseService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}

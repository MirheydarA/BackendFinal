using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Duty;
using Business.ViewModels.Admin.Slider;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DutyController : Controller
    {
        private readonly IDutyRepository _dutyRepository;
        private readonly IDutyService _dutyService;

        public DutyController(IDutyRepository dutyRepository, IDutyService dutyService)
        {
            _dutyRepository = dutyRepository;
            _dutyService = dutyService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new DutyIndexVM
            {
                Duties = await _dutyRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DutyCreateVM model)
        {
            var isSucceded = await _dutyService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var duty = await _dutyRepository.GetByIdAsync(id);
            if (duty == null) return NotFound("Peşə tapilmadi");

            var model = new DutyUpdateVM()
            {
                Name = duty.Name,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, DutyUpdateVM model)
        {
            var isSucceded = await _dutyService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _dutyService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }




    }
}

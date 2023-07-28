using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.DepartmentComponent;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentComponentController : Controller
    {
        private readonly IDepartmentComponentRepository _departmentComponenetRepository;
        private readonly IDepartmentComponentService _departmentComponentService;

        public DepartmentComponentController(IDepartmentComponentRepository departmentComponenetRepository, IDepartmentComponentService departmentComponentService)
        {
            _departmentComponenetRepository = departmentComponenetRepository;
            _departmentComponentService = departmentComponentService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new DepartmentComponentIndexVM()
            {
               DepartmentComponents  = await _departmentComponenetRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentComponentCreateVM model)
        {
            var isSucceded = await _departmentComponentService.DepartmentComponentCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var slider = await _departmentComponenetRepository.GetByIdAsync(id);
            if (slider == null) return NotFound("Slider tapilmadi");

            var model = new DepartmentComponentUpdateVM()
            {
                Title = slider.Title,
                Description = slider.Description,
                //Photoname = slider.Photoname
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, DepartmentComponentUpdateVM model)
        {
            var isSucceded = await _departmentComponentService.DepartmentComponentUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _departmentComponentService.DepartmentComponentDeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}


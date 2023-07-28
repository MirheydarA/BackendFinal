using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.AboutUs;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly IAbuotUsRepository _abuotUsRepository;
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAbuotUsRepository abuotUsRepository, IAboutUsService aboutUsService)
        {
            _abuotUsRepository = abuotUsRepository;
            _aboutUsService = aboutUsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new AboutUsIndexVM
            {
                AboutUss = await _abuotUsRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutUsCreateVM model)
        {
            var isSucceded = await _aboutUsService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var aboutUs = await _abuotUsRepository.GetByIdAsync(id);
            if (aboutUs == null) return NotFound("About Us tapilmadi");

            var model = new AboutUsUpdateVM()
            {
                Title = aboutUs.Title,
                Description = aboutUs.Description,
                //MainPhotoname = aboutUs.MainPhotoname,
                //Photoname = aboutUs.Photoname
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, AboutUsUpdateVM model) 
        {
            var isSucceded = await _aboutUsService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _aboutUsService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _aboutUsService.DetailsAsync(id);
            if (model != null) return View(model);

            return RedirectToAction(nameof(Index), "dashboard");
        }
    }
}

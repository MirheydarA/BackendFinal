using Business.Services.Abstract;
using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Category;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderRepository _sliderRepository;

        public SliderController(ISliderService sliderService, ISliderRepository sliderRepository )
        {
            _sliderService = sliderService;
            _sliderRepository = sliderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new SliderIndexVM()
            {
                Sliders = await _sliderRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(SliderCreateVM model)
        {
            var isSucceded = await _sliderService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");
            
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null) return NotFound("Slider tapilmadi");

            var model = new SliderUpdateVM()
            {
                Title = slider.Title,
                Subtitle = slider.Subtitle,
               
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderUpdateVM model)
        {
            var isSucceded = await _sliderService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _sliderService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }



    }
}

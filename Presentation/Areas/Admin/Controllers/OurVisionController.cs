using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.OurVision;
using Business.ViewModels.Admin.OurVisionComponent;
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
            var model = new OurVisionIndexVM()
            {
                OurVisions = await _ourVisionRepository.GetAllAsync()
            };
            return View(model);
        }

        
    }
}

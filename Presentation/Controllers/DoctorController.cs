using Business.Services.Abstract.User;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPaginator _paginator;

        public DoctorController(IDoctorService doctorService, IPaginator paginator)
        {
            _doctorService = doctorService;
            _paginator = paginator;
        }
        public async Task<IActionResult> Index(DoctorIndexVM model)
        {

            return View(await _doctorService.IndexGetAllAsync(model));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _doctorService.DetailsAsync(id);
            if (model != null) return View(model);

            return NotFound("Nese duz getmedi");
        }
    }
}

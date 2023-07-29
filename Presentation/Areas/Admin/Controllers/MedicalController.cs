using Business.ViewModels.Admin.Medical;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class MedicalController : Controller
    {
        private readonly IMedicalRepository _medicalRepository;

        public MedicalController(IMedicalRepository medicalRepository)
        {
            _medicalRepository = medicalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MedicalndexVM
            {
                Medicals = await _medicalRepository.GetAllAsync()
            };
            return View(model);
        }
    }
}

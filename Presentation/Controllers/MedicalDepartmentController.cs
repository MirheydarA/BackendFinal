using Business.Services.Abstract.User;
using Business.ViewModels.MedicalDepartment;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MedicalDepartmentController : Controller
    {
        private readonly IMedicalDepartmentService _medicalDepartmentService;

        public MedicalDepartmentController(IMedicalDepartmentService medicalDepartmentService)
        {
            _medicalDepartmentService = medicalDepartmentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new MedicalDepartmentIndexVM()
            {
                DepartmentComponents = await _medicalDepartmentService.GetDepartmentComponentsAsync(),
                Medicals = await _medicalDepartmentService.GetMedicalAsync()
            };
            return View(model);
        }
    }
}

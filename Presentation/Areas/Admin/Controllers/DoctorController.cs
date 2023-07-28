using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Doctor;
using Business.ViewModels.Admin.Slider;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorService _doctorService;
        private readonly IDutyRepository _dutyRepository;

        public DoctorController(IDoctorRepository doctorRepository, IDoctorService doctorService, IDutyRepository dutyRepository)
        {
            _doctorRepository = doctorRepository;
            _doctorService = doctorService;
            _dutyRepository = dutyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetDoctorsWithDuty()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorRepository.GetDoctorsWithDuty();

            var model = new DoctorCreateVM();
            var list = await _dutyRepository.GetAllAsync();

            model.Duties = list.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateVM model)
        {
            var doctors = await _doctorRepository.GetDoctorsWithDuty();

            var isSucceded = await _doctorService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            var list = await _dutyRepository.GetAllAsync();
            model.Duties = list.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //var doctors = await _doctorRepository.GetDoctorsWithDuty();

          
            var doctor = await _doctorRepository.GetDoctorAndDuty(id);
            if (doctor == null) return NotFound("Hekim tapilmadi");
            var list = await _dutyRepository.GetAllAsync();




            var model = new DoctorUpdateVM
            {
                Duties = list.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),


                Name = doctor.Name,
               //Photoname = doctor.Photoname,
                Qualification = doctor.Qualification,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                WorkingTime = doctor.WorkingTime,
                Description = doctor.Description,
                DutyId = doctor.DutyId,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, DoctorUpdateVM model)
        {

            var isSucceded = await _doctorService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _doctorService.DetailsAsync(id);
            if (model != null) return View(model);

            return RedirectToAction(nameof(Index), "dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _doctorService.DeleteAsync(id);
            if (isSucceded) { return RedirectToAction(nameof(Index)); }

            return NotFound();
        }

    }
}

using Business.Services.Abstract.User;
using Business.Services.Utilities.Abstract;
using Business.ViewModels;
using Business.ViewModels.Doctor;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.User
{

    public class DoctorService : IDoctorService
    {
        private readonly DataAccess.Repositories.Abstract.User.IDoctorRepository _doctorRepository;
        private readonly IDutyRepository _dutyRepository;
        private readonly IPaginator _paginator;

        public DoctorService(DataAccess.Repositories.Abstract.User.IDoctorRepository doctorRepository, IDutyRepository dutyRepository, IPaginator paginator)
        {
            _doctorRepository = doctorRepository;
            _dutyRepository = dutyRepository;
            _paginator = paginator;
        }

        public async Task<DoctorIndexVM> IndexGetAllAsync(DoctorIndexVM model)
        {
            var doctors = await _doctorRepository.FilterByName(model.Name).ToListAsync();

            model = new DoctorIndexVM
            {
                Doctors = _paginator.GetPagedList(doctors, model.CurrentPage, model.PageSize)
            };

            return model;
        }

        public async Task<DoctorDetailsVM>? DetailsAsync(int id)
        {

            var doctor = await _doctorRepository.GetDoctorsWithDuty(id);
            if (doctor is null) return null;

            var doctorWithDuty = new DoctorDetailsVM
            {
                Name = doctor.Name,
                Description = doctor.Description,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Qualification = doctor.Qualification,
                WorkingTime = doctor.WorkingTime,
                Duty = doctor.Duty,
                Photoname = doctor.Photoname
            };

            return doctorWithDuty;
        }
    }
}

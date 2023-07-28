using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Doctor;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IDutyRepository _dutyRepository;

        public DoctorService(IDoctorRepository doctorRepository,IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService, IDutyRepository dutyRepository)
        {
            _doctorRepository = doctorRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _dutyRepository = dutyRepository;
        }
        public async Task<bool> CreateAsync(DoctorCreateVM model)
        {
           var list = await _dutyRepository.GetAllAsync();

           model.Duties = list.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();


            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photo, 900))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 100kb dan boyukdur");
                return false;
            }

            var doctor = new Doctor
            {
                Name = model.Name,
                Photoname = _fileService.Upload(model.Photo),
                Qualification = model.Qualification,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                WorkingTime = model.WorkingTime,
                Description = model.Description,
                DutyId = model.DutyId,
                CreatedAt = DateTime.Now,
            };

            await _doctorRepository.CreateAsync(doctor);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, DoctorUpdateVM model)
        {
            var list = await _dutyRepository.GetAllAsync();

            var duties = list.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            if (!_modelState.IsValid) return false;

            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li hekim yoxdur");
                return false;
            }

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.Photo, 900))
                {
                    _modelState.AddModelError("Photo", "Sekilin olcusu 900kb dan boyukdur");
                    return false;
                }
                doctor.Photoname = _fileService.Upload(model.Photo);
            }

            var duty = await _dutyRepository.GetByIdAsync(model.DutyId);
            if (duty is null)
            {
                _modelState.AddModelError("DutyId", "Bele peşə mövcud deyil");
            }

            doctor.Name = model.Name;
            doctor.Description = model.Description;
            doctor.Qualification = model.Qualification;
            doctor.WorkingTime = model.WorkingTime;
            doctor.PhoneNumber = model.PhoneNumber;
            doctor.Email = model.Email;
            doctor.DutyId = model.DutyId;
            doctor.ModfiedAt = DateTime.Now;
            

            _doctorRepository.Update(doctor);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li hekim yoxdur");
                return false;
            }

            doctor.IsDeleted = true;

            _doctorRepository.Delete(doctor);
            await _unitOfWork.CommitAsync();
            return true;
        }


        public async Task<DoctorDetailsVM>? DetailsAsync(int id)
        {
            var doctor = await _doctorRepository.GetDoctorAndDuty(id);
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
                Photo = doctor.Photoname
            };

            return doctorWithDuty;
        }
    }
}

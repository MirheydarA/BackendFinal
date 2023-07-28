using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.DepartmentComponent;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class DepartmentComponentService : IDepartmentComponentService
    {
        private readonly IDepartmentComponentRepository _departmentComponentRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public DepartmentComponentService(IDepartmentComponentRepository  departmentComponentRepository , IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _departmentComponentRepository = departmentComponentRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> DepartmentComponentCreateAsync(DepartmentComponentCreateVM model)
        {
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

            var departmentComponent = new DepartmentComponent
            {
                Title = model.Title,
                Description = model.Description,
                Photoname = _fileService.Upload(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _departmentComponentRepository.CreateAsync(departmentComponent);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DepartmentComponentUpdateAsync(int id, DepartmentComponentUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var slider = await _departmentComponentRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

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

            slider.Title = model.Title;
            slider.Description = model.Description;
            slider.Photoname = _fileService.Upload(model.Photo);
            slider.ModfiedAt = DateTime.Now;

            _departmentComponentRepository.Update(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DepartmentComponentDeleteAsync(int id)
        {
            var departmentComponent = await _departmentComponentRepository.GetByIdAsync(id);
            if ( departmentComponent is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            departmentComponent.IsDeleted = true;

            _departmentComponentRepository.Delete(departmentComponent);
            await _unitOfWork.CommitAsync();
            return true;
        }

        
    }
}

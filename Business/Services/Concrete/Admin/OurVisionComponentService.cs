using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.OurVisionComponent;
using Business.ViewModels.Admin.Slider;
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
    public class OurVisionComponentService : IOurVisionComponentService
    {
        private ModelStateDictionary _modelState;
        private readonly IOurVisionComponentRepository _ourVisionComponentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public OurVisionComponentService(IOurVisionComponentRepository ourVisionComponentRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _ourVisionComponentRepository = ourVisionComponentRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(OurVisionComponentCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photoname))
            {
                _modelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photoname, 900))
            {
                _modelState.AddModelError("Photo", "Sekilin olcusu 100kb dan boyukdur");
                return false;
            }

            var ourVisionComponent = new OurVisionComponent()
            {
                Title = model.Title,
                Description = model.Description,
                Photoname = _fileService.Upload(model.Photoname),
                CreatedAt = DateTime.Now,
            };

            await _ourVisionComponentRepository.CreateAsync(ourVisionComponent);
            await _unitOfWork.CommitAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(int id, OurVisionComponentUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var ourVisionComponent = await _ourVisionComponentRepository.GetByIdAsync(id);
            if (ourVisionComponent is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li komponent yoxdur");
                return false;
            }

            if (!_fileService.IsImage(model.Photoname))
            {
                _modelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photoname, 900))
            {
                _modelState.AddModelError("Photo", "Sekilin olcusu 900kb dan boyukdur");
                return false;
            }

            ourVisionComponent.Title = model.Title;
            ourVisionComponent.Description = model.Description;
            ourVisionComponent.Photoname = _fileService.Upload(model.Photoname);
            ourVisionComponent.ModfiedAt = DateTime.Now;

            _ourVisionComponentRepository.Update(ourVisionComponent);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var slider = await _ourVisionComponentRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li komponent yoxdur");
                return false;
            }

            slider.IsDeleted = true;

            _ourVisionComponentRepository.Delete(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

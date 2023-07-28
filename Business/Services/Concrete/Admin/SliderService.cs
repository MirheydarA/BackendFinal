using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public SliderService(ISliderRepository sliderRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(SliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photoname))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photoname, 900))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 100kb dan boyukdur");
                return false;
            }

            var slider = new Slider
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Photoname = _fileService.Upload(model.Photoname),
                CreatedAt = DateTime.Now,
            };

            await _sliderRepository.CreateAsync(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, SliderUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

           

            if (model.Photoname != null)
            {
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
                slider.Photoname = _fileService.Upload(model.Photoname);
            }

            slider.Title = model.Title;
            slider.Subtitle = model.Subtitle;
            slider.ModfiedAt = DateTime.Now;

            _sliderRepository.Update(slider);
            await _unitOfWork.CommitAsync();
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            slider.IsDeleted = true;

            _sliderRepository.Delete(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }




    }
}

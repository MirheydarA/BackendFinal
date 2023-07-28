using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.AboutUs;
using Business.ViewModels.Admin.User;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAbuotUsRepository _abuotUsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private ModelStateDictionary _modelState;

        public AboutUsService(IAbuotUsRepository abuotUsRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _abuotUsRepository = abuotUsRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(AboutUsCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.MainPhotoName))
            {
                _modelState.AddModelError("MainPhotoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.MainPhotoName, 900))
            {
                _modelState.AddModelError("MainPhotoname", "Sekilin olcusu 100kb dan boyukdur");
                return false;
            }

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

            var aboutUs = new AboutUs
            {
                Title = model.Title,
                Description = model.Description,
                MainPhotoname = _fileService.Upload(model.MainPhotoName),
                Photoname = _fileService.Upload(model.Photoname),
                CreatedAt = DateTime.Now,
            };

            _abuotUsRepository.CreateAsync(aboutUs);
            _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, AboutUsUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var aboutUs = await _abuotUsRepository.GetByIdAsync(id);
            if (aboutUs is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li about us yoxdur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhotoname))
            {
                _modelState.AddModelError("MainPhotoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.MainPhotoname, 900))
            {
                _modelState.AddModelError("MainPhotoname", "Sekilin olcusu 900kb dan boyukdur");
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

            aboutUs.Title = model.Title;
            aboutUs.Description = model.Description;
            aboutUs.MainPhotoname = _fileService.Upload(model.MainPhotoname);
            aboutUs.Photoname = _fileService.Upload(model.Photoname);
            aboutUs.ModfiedAt = DateTime.Now;

            _abuotUsRepository.Update(aboutUs);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var slider = await _abuotUsRepository.GetByIdAsync(id);
            if (slider is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            slider.IsDeleted = true;

            _abuotUsRepository.Delete(slider);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<AboutUsDetailsVM>? DetailsAsync(int id)
        {
            var aboutUs = await _abuotUsRepository.GetByIdAsync(id);

            if (aboutUs is null) return null;

            var newabout = new AboutUsDetailsVM
            {
                Title = aboutUs.Title,
                Description = aboutUs.Description,
                MainPhotoname = aboutUs.MainPhotoname,
                Photoname = aboutUs.Photoname,
            };

            return newabout;
        }
    }
}

using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.WhyChoose;
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
    public class WhyChooseService : IWhyChooseService
    {
        private ModelStateDictionary _modelState;
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public WhyChooseService(IWhyChooseRepository whyChooseRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _whyChooseRepository = whyChooseRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(WhyChooseCreateVM model)
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

            var whyChoose = new WhyChoose
            {
                Description = model.Description,
                Photoname = _fileService.Upload(model.Photo),
                CreatedAt = DateTime.Now,
            };

            await _whyChooseRepository.CreateAsync(whyChoose);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id,WhyChooseUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var whyChoose = await _whyChooseRepository.GetByIdAsync(id);
            if (whyChoose is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
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
                whyChoose.Photoname = _fileService.Upload(model.Photo);
            }

            whyChoose.Description = model.Description;
            whyChoose.ModfiedAt = DateTime.Now;

            _whyChooseRepository.Update(whyChoose);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var whyChoose = await _whyChooseRepository.GetByIdAsync(id);
            if (whyChoose is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            whyChoose.IsDeleted = true;

            _whyChooseRepository.Delete(whyChoose);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

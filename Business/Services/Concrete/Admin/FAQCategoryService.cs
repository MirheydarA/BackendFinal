using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.FAQCategory;
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

    public class FAQCategoryService : IFAQCategoryService
    {
        private readonly IFAQCategoryRepository _FAQCategoryRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;

        public FAQCategoryService(IFAQCategoryRepository FAQCategoryRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork)
        {
            _FAQCategoryRepository = FAQCategoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
        }

        public async Task<FAQCategoryIndexVM> GetAllAsync()
        {
            var model = new FAQCategoryIndexVM();
            model.FAQCategories = await _FAQCategoryRepository.GetAllAsync();
            return model;
        }

        public async Task<bool> CreateAsync(FAQCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var FAQCategory = new FAQCategory
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
            };

            await _FAQCategoryRepository.CreateAsync(FAQCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> PostUpdateAsync(int id, FAQCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var fAQCategory = await _FAQCategoryRepository.GetByIdAsync(id);
            if (fAQCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li Faq kateqoriya yoxdur");
                return false;
            }


            fAQCategory.Title = model.Title;
            fAQCategory.Description = model.Description;
            fAQCategory.ModfiedAt = DateTime.Now;

            _FAQCategoryRepository.Update(fAQCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
        //////////////////////
        public async Task<FAQCategoryUpdateVM?> GetUpdateAsync(int id)
        {

            var fAQCategory = await _FAQCategoryRepository.GetByIdAsync(id);

            if (fAQCategory is null) return null;

            var model = new FAQCategoryUpdateVM
            {
                Title = fAQCategory.Title,
                Description = fAQCategory.Description,
            };


            return model;
        }

        ///////////////////////////////
        public async Task<bool> DeleteAsync(int id)
        {
            var fAQCategory = await _FAQCategoryRepository.GetByIdAsync(id);
            if (fAQCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li kateqoriya yoxdur");
                return false;
            }

            fAQCategory.IsDeleted = true;

            _FAQCategoryRepository.Delete(fAQCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

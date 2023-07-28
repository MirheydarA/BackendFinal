using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.FAQCategory;
using Business.ViewModels.Admin.ProductCategory;
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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,
                                      IActionContextAccessor actionContextAccessor,
                                      IUnitOfWork unitOfWork,
                                      IFileService fileService)
        {
            _productCategoryRepository = productCategoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<ProductCategoryIndexVM> GetAllAsync()
        {
            var model = new ProductCategoryIndexVM();
            model.ProductCategories = await _productCategoryRepository.GetAllAsync();
            return model;
        }

        public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var productCategory = new ProductCategory
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
            };

            await _productCategoryRepository.CreateAsync(productCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<ProductCategoryUpdateVM?> GetUpdateAsync(int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            if (productCategory is null) return null;

            var model = new ProductCategoryUpdateVM
            {
                Name = productCategory.Name,
            };

            return model;
        }

        public async Task<bool> PostUpdateAsync(int id, ProductCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var productCategory = await _productCategoryRepository.GetByIdAsync(id);
            if (productCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li Mehsul kateqoriyasi yoxdur");
                return false;
            }


            productCategory.Name = model.Name;
            productCategory.ModfiedAt = DateTime.Now;

            _productCategoryRepository.Update(productCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);
            if (productCategory is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li Mehsul kateqoriyasi yoxdur");
                return false;
            }

            productCategory.IsDeleted = true;

            _productCategoryRepository.Delete(productCategory);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

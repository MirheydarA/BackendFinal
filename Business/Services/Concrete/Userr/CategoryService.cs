using Business.Services.Abstract.User;
using Business.ViewModels.Category;
using Common.Entities;
using DataAccess.Repositories.Abstract.User;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.User
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public CategoryService(ICategoryRepository categoryRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var category = await _categoryRepository.GetByTitle(model.Title);
            if (category == null)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya movcuddur");
                return false;
            }

            category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.FAQCategory;
using Business.ViewModels.Admin.Question;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFAQCategoryRepository _fAQCategoryRepository;

        public QuestionService(IQuestionRepository questionRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFAQCategoryRepository fAQCategoryRepository)
        {
            _questionRepository = questionRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _fAQCategoryRepository = fAQCategoryRepository;
        }

        public async Task<QuestionIndexVM> IndexGetAllAsync()
        {
            var model = new QuestionIndexVM();
            model.Questions = await _questionRepository.GetQuestionWithCAtegory();
            return model;
        }

        public async Task<QuestionCreateVM> GetCreateAsync()
        {
            var questions = await _questionRepository.GetQuestionWithCAtegory();
            var model = new QuestionCreateVM();
            var listCategory = await _fAQCategoryRepository.GetAllAsync();

            model.FAQCategories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString(),
            }).ToList();

            return model;
        }

        public async Task<bool> PostCreateAsync(QuestionCreateVM model)
        {
            var list = await _fAQCategoryRepository.GetAllAsync();

            model.FAQCategories = list.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString(),
            }).ToList();
            

            if (!_modelState.IsValid) return false;

            var question = new Question
            {
                Content = model.Content,
                Answer = model.Answer,
                FAQCategoryId = model.FAQCategoryId,
                CreatedAt = DateTime.Now,
            };

            await _questionRepository.CreateAsync(question);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<QuestionUpdateVM?> GetUpdateAsync(int id)
        {
            var question = await _questionRepository.GetQuestionAndCategory(id);
            if (question is null) return null;

            var listCategory = await _fAQCategoryRepository.GetAllAsync();

            var model = new QuestionUpdateVM
            {
                FAQCategories = listCategory.Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString(),
                }).ToList(),

                Content = question.Content,
                Answer = question.Answer,
                FAQCategoryId = question.FAQCategoryId,

            };

            return model;
        }

        public async Task<bool> PostUpdateAsync(int id, QuestionUpdateVM model)
        {
            var listCategory = await _fAQCategoryRepository.GetAllAsync();
            var categories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString(),
            }).ToList();

            if (!_modelState.IsValid) return false;

            var question = await _questionRepository.GetByIdAsync(id);
            if (question is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li sual yoxdur");
                return false;
            }

            

            var duty = await _questionRepository.GetByIdAsync(model.FAQCategoryId);
            if (duty is null)
            {
                _modelState.AddModelError("FAQCategoryId", "Bele kateqoriya mövcud deyil");
            }

            question.Content = model.Content;
            question.Answer = model.Answer;
            question.FAQCategoryId = model.FAQCategoryId;
            question.ModfiedAt = DateTime.Now;


            _questionRepository.Update(question);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li sual yoxdur");
                return false;
            }

            question.IsDeleted = true;

            _questionRepository.Delete(question);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}

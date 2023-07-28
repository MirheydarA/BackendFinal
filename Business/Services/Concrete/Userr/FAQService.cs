using Business.Services.Abstract.User;
using Business.ViewModels.FAQCategory;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.User
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _fAQCategoryRepository;
        private readonly IQuestionRepository _questionRepository;

        public FAQService(IFAQRepository fAQCategoryRepository, IQuestionRepository questionRepository)
        {
            _fAQCategoryRepository = fAQCategoryRepository;
            _questionRepository = questionRepository;
        }

        public async Task<FAQIndexVM> GetAllAsync()
        {
            var model = new FAQIndexVM()
            {
                fAQCategories = await _fAQCategoryRepository.GetAllAsync(),
                Questions = await _questionRepository.GetAllAsync()
            };
            return model;
        }
    }
}

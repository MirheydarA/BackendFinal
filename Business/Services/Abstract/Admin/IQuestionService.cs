using Business.ViewModels.Admin.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IQuestionService
    {
        Task<QuestionIndexVM> IndexGetAllAsync();
        Task<QuestionCreateVM> GetCreateAsync();
        Task<bool> PostCreateAsync(QuestionCreateVM model);
        Task<QuestionUpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id,QuestionUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

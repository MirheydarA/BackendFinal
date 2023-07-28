using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Admin
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetQuestionWithCAtegory();
        Task<Question> GetQuestionAndCategory(int id);
    }
}

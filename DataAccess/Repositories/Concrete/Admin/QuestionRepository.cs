using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Question> GetQuestionAndCategory(int id)
        {
            var question = await _context.Questions.Include(x => x.FAQCategory).FirstOrDefaultAsync(x => x.Id == id);
            return question;
        }

        public async Task<List<Question>> GetQuestionWithCAtegory()
        {
            var questions = await _context.Questions.Include(x => x.FAQCategory).Where(x => !x.IsDeleted).ToListAsync();
            return questions;
        }
    }
}

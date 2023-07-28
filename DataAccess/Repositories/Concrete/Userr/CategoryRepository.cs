using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.User;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.User
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetByTitle(string title)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == title.ToLower().Trim());
        }
    }
}

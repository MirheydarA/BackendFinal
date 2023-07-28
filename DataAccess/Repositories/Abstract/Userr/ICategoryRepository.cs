using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.User
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByTitle(string title);
    }
}

using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.User;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.User
{
    public class FAQRepository : Repository<FAQCategory>, IFAQRepository
    {
        private readonly AppDbContext _context;

        public FAQRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class DepartmentComponentRepository : Repository<DepartmentComponent>, IDepartmentComponentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

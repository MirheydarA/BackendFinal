using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Userr;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Userr
{
    public class MedicalDepartmentRepository : Repository<Medical>, IMedicalDepartmentRepository
    {
        private readonly AppDbContext _context;

        public MedicalDepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

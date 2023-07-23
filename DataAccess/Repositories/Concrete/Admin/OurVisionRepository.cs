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
    public class OurVisionRepository : Repository<OurVision>, IOurVisionRepository
    {
        private readonly AppDbContext _context;

        public OurVisionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

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
    public class OurVisionComponentRepository : Repository<OurVisionComponent>, IOurVisionComponentRepository
    {
        private readonly AppDbContext _context;

        public OurVisionComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public  ICollection<OurVisionComponent> GetActive()
        {
           return  _context.OurVisionComponents.Where(c => !c.IsDeleted).ToList();

        }
    }
}

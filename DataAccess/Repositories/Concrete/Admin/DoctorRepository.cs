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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctorAndDuty(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Duty).FirstOrDefaultAsync(d => d.Id == id);
           return doctor;
        }

        public async Task<List<Doctor>> GetDoctorsWithDuty()
        {
            var doctors = await _context.Doctors.Include(d => d.Duty).Where(d =>  !d.IsDeleted).ToListAsync();
            return doctors;
        }
    }
}

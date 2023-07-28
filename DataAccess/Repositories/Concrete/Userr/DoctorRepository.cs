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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Doctor> FilterByName(string? name)
        {
            var doctors = !string.IsNullOrEmpty(name) ? _context.Doctors.Include(d => d.Duty).Where(d => d.Name.Contains(name)) : _context.Doctors.Include(d => d.Duty).Where(d => !d.IsDeleted);
            return doctors;
        }

        public async Task<Doctor> GetDoctorsWithDuty(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Duty).Where(d => !d.IsDeleted).FirstOrDefaultAsync(d => d.Id == id);
            return doctor;
        }

        public async Task<List<Doctor>> GetDoctorsWithDutyAndContainName(string name)
        {
            var doctors = await _context.Doctors.Include(d => d.Duty).Where(d => !d.IsDeleted).ToListAsync();
            doctors = doctors.Where(d => d.Name.Contains(name)).ToList();
            return doctors;
        }
    }
}

using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.User
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> GetDoctorsWithDutyAndContainName(string name);
        Task<Doctor> GetDoctorsWithDuty(int id);
        IQueryable<Doctor> FilterByName(string? name);

    }
}

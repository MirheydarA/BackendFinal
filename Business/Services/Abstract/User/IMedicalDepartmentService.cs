using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IMedicalDepartmentService
    {
        Task<List<Medical>> GetMedicalAsync();
        Task<List<DepartmentComponent>> GetDepartmentComponentsAsync();
    }
}

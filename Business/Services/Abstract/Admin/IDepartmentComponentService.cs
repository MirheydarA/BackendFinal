using Business.ViewModels.Admin.DepartmentComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IDepartmentComponentService
    {
        public Task<bool> DepartmentComponentCreateAsync(DepartmentComponentCreateVM model);
        public Task<bool> DepartmentComponentUpdateAsync(int id, DepartmentComponentUpdateVM model); 
        public Task<bool> DepartmentComponentDeleteAsync(int id);
    }
}

using Business.ViewModels.Admin.Duty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IDutyService
    {
        Task<bool> CreateAsync(DutyCreateVM model);
        Task<bool> UpdateAsync(int id,DutyUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

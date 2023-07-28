using Business.ViewModels.Admin.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IDoctorService
    {
        Task<bool> CreateAsync(DoctorCreateVM model);
        Task<bool> UpdateAsync(int id,DoctorUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<DoctorDetailsVM> DetailsAsync(int id);

    }
}

using Business.ViewModels.Doctor;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> IndexGetAllAsync(DoctorIndexVM model);
        Task<DoctorDetailsVM> DetailsAsync(int id);

    }
}

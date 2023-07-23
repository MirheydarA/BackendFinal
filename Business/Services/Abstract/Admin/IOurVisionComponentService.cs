using Business.ViewModels.Admin.OurVisionComponent;
using Business.ViewModels.Admin.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IOurVisionComponentService
    {
        Task<bool> CreateAsync(OurVisionComponentCreateVM model);
        Task<bool> UpdateAsync(int id, OurVisionComponentUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

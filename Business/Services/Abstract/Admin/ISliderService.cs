using Business.ViewModels.Admin.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface ISliderService
    {
        Task<bool> CreateAsync(SliderCreateVM model);
        Task<bool> UpdateAsync(int id, SliderUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

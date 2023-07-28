using Business.ViewModels.Admin.WhyChoose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IWhyChooseService
    {
        Task<bool> CreateAsync(WhyChooseCreateVM model);
        Task<bool> UpdateAsync(int id, WhyChooseUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

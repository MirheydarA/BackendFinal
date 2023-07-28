using Business.ViewModels.Admin.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IAboutUsService
    {
        public Task<bool> CreateAsync(AboutUsCreateVM model);
        public Task<bool> UpdateAsync(int id,AboutUsUpdateVM model);
        public Task<bool> DeleteAsync(int id);
        public Task<AboutUsDetailsVM> DetailsAsync(int id);
    }
}

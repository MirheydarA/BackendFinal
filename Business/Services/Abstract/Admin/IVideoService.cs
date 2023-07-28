using Business.ViewModels.Admin.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IVideoService
    {
        Task<bool> CreateAsync(VideoCreateVM model);
        Task<bool> UpdateAsync(int id,VideoUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}

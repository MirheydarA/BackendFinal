using Business.ViewModels.Admin.FAQCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IFAQCategoryService 
    {
        Task<FAQCategoryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(FAQCategoryCreateVM model);
        Task<bool> PostUpdateAsync(int id, FAQCategoryUpdateVM model);
        Task<FAQCategoryUpdateVM?> GetUpdateAsync(int id);
        Task<bool> DeleteAsync(int id);

    }
}

using Business.ViewModels.Admin.FAQCategory;
using Business.ViewModels.Admin.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<bool> PostUpdateAsync(int id, ProductCategoryUpdateVM model);
        Task<ProductCategoryUpdateVM?> GetUpdateAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

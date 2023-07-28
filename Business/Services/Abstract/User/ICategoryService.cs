using Business.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.User
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateVM model);
    }
}

using Business.ViewModels.Admin.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IUserService
    {
        public Task<bool> CreateAsync(UserCreateVM model);
        public Task<bool> UpdateAsync(string id,UserUpdateVM model);
        public Task<bool> DeleteAsync(string id);
        public Task<UserDetailsVM> DetailsAsync(string id);
    }
}

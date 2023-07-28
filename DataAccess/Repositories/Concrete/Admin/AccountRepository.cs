using Business.Services.Constants;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Common.Entities.User> _userManager;
        private readonly SignInManager<Common.Entities.User> signInManager;

        public AccountRepository(UserManager<Common.Entities.User> userManager, SignInManager<Common.Entities.User> signInManager)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<bool> HasAccessToAdminPanelAsync(Common.Entities.User user)
        {
            if (await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()) ||
                await _userManager.IsInRoleAsync(user, UserRoles.Superadmin.ToString()) ||
                await _userManager.IsInRoleAsync(user, UserRoles.HR.ToString()))
            {
                return true;
            }

            return false;
        }

        
    }


}

using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Account;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    [Area("Admin")]
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private ModelStateDictionary _modelState;

        public AccountService(IActionContextAccessor actionContextAccessor,UserManager<User> userManager, SignInManager<User> signInManager, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _modelState.AddModelError(string.Empty, "Email or Password is incorrect");
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, "Email or Password is incorrect");
                return false;
            }

            if (!await _accountRepository.HasAccessToAdminPanelAsync(user))
            {
                _modelState.AddModelError(string.Empty, "You don't have permission to admin panel");
                return false;
            }

            return true;
        }
    }
}

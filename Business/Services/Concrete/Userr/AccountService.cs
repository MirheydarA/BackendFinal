using Business.Services.Abstract.User;
using Business.Services.Constants;
using Business.ViewModels.Account;
using Common.Entities;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Services.Concrete.User
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Common.Entities.User> _userManager;
        private readonly SignInManager<Common.Entities.User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ModelStateDictionary _modelState;

        public AccountService(IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              UserManager<Common.Entities.User> userManager,
                              SignInManager<Common.Entities.User> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> RegisterAsync(AccountRegisterVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = new Common.Entities.User
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
            };

            string pattern = @"^994(?:50|51|55|70|77|99|050|)\d{7}$";

            Regex regexnumber = new Regex(pattern);

            if (!regexnumber.IsMatch(user.PhoneNumber))
            {
                _modelState.AddModelError("PhoneNumber", "Telefon nomresi duzgun formatda deyil");
                return false;
            }

            string emailpattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regexemail = new Regex(emailpattern);
            if (!regexemail.IsMatch(user.Email))
            {
                _modelState.AddModelError("Email", "Email duzgun formatda deyil");
                return false;
            }




            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError("Password", error.Description);
                }
                return false;
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            return true;
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

            return true;
        }

    }
}

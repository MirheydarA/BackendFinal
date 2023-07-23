﻿using Business.Services.Abstract;
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
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ModelStateDictionary _modelState;

        public AccountService(IActionContextAccessor actionContextAccessor, 
                              IUnitOfWork unitOfWork, 
                              UserManager<User> userManager, 
                              SignInManager<User> signInManager, 
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

            var user = new User
            {
                Email = model.Email,
                UserName = model.Username,
                Fullname = model.Fullname,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError(string.Empty, error.Description);
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

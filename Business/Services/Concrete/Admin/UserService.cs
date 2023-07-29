using Azure.Core;
using Business.Services.Abstract.Admin;
using Business.Services.Constants;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.User;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class UserService : IUserService
    {
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Common.Entities.User> _userManager;
        private readonly IFileService _fileService;

        public UserService(IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<Common.Entities.User> userManager)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> CreateAsync(UserCreateVM model)
        {
            model.Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.User.ToString() &&
                                                              r.Name != UserRoles.Superadmin.ToString())
                                                  .Select(r => new SelectListItem
                                                  {
                                                      Value = r.Id,
                                                      Text = r.Name
                                                  })
                                                   .ToListAsync();


            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user is not null)
            {
                _modelState.AddModelError("Username", "Bu adda istifadeci movcuddur");
                return false;
            }

            user = new Common.Entities.User
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.Fullname
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

            foreach (var roleid in model.RolesIds)
            {
                var role = await _roleManager.FindByIdAsync(roleid);
                if (role is null)
                {
                    _modelState.AddModelError("RoleIds", "rol movcud deyil");
                    return false;
                }

                result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        _modelState.AddModelError(string.Empty, error.Description);

                    return false;
                }
            }
            return true;
        }

        public async Task<bool> UpdateAsync(string id,UserUpdateVM model)
        {
            model.Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.User.ToString() &&
                                                             r.Name != UserRoles.Superadmin.ToString())
                                                 .Select(r => new SelectListItem
                                                 {
                                                     Value = r.Id,
                                                     Text = r.Name
                                                 })
                                                  .ToListAsync();

            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return false;

            user.FullName = model.Fullname;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Username;
            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError(string.Empty, error.Description);
                }
                return false;
            }

            var userRoles = new List<IdentityRole>();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    throw new Exception("Rol movcud deyil");
                }
                userRoles.Add(role);
            }
            var removedIds = userRoles.FindAll(x => !model.RolesIds.Contains(x.Id)).ToList();

            foreach (var removedRole in removedIds)
            {
                await _userManager.RemoveFromRoleAsync(user, removedRole.Name);
            }

            foreach (var roleId in model.RolesIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    _modelState.AddModelError("RolesIds", "Rol movcud deyil");
                    return false;
                }

                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                else if (!userRoles.Any(r => r.Id == role.Id))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return false;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }
            return true;
        }

        public async Task<UserDetailsVM>? DetailsAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var userWithRoles = new UserDetailsVM
            {
                UserName = user.UserName,
                Email = user.Email,
               // FullName = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList(),
            };

            return userWithRoles;
        }
    }
}

using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class RoleService : IRoleService
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private ModelStateDictionary _modelState;

		public RoleService(IActionContextAccessor actionContextAccessor, RoleManager<IdentityRole> roleManager)
        {
			_modelState = actionContextAccessor.ActionContext.ModelState;
			_roleManager = roleManager;
		}

		public async Task<bool> CreateAsync(RoleCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var role = await _roleManager.FindByNameAsync(model.Name);
			if (role is not null)
			{
				_modelState.AddModelError("Name", "BU adda rol movcuddur");
				return false;
			}

			role = new IdentityRole
			{
				Name = model.Name
			};
			var result = await _roleManager.CreateAsync(role);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					_modelState.AddModelError(string.Empty, error.Description);
				}
				return false;
			}

			return true;
		}


		public async Task<bool> UpdateAsync(string id, RoleUpdateVM model)
		{
			if (!_modelState.IsValid) return false;

			var role = await _roleManager.FindByIdAsync(id);

			if (role is null) return false;
			if (await _roleManager.Roles.AnyAsync(r => r.Name == model.Name))
			{
				_modelState.AddModelError("Name", "Bu adda rol movcuddur");
				return false;
			}

			role.Name = model.Name;

			await _roleManager.UpdateAsync(role);
			return true;
		}

		public async Task<bool> DeleteAsync(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return false;

			var result = await _roleManager.DeleteAsync(role);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					throw new Exception(error.Description);
				}
			}
			return true;
		}
	}
}

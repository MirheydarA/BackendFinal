using Business.Services.Abstract.Admin;
using Business.Services.Constants;
using Business.ViewModels.Admin.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Superadmin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IRoleService _roleService;

		public RoleController(RoleManager<IdentityRole> roleManager, IRoleService roleService)
        {
			_roleManager = roleManager;
			_roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = new RoleIndexVM
			{
				Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.Superadmin.ToString()).ToListAsync()
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoleCreateVM model)
		{
			var isSucceded = await _roleService.CreateAsync(model);
			if (isSucceded) return RedirectToAction("index");

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			var model = new RoleUpdateVM
			{
				Name = role.Name
			};

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update(string id, RoleUpdateVM model)
		{
			var isSucceded = await _roleService.UpdateAsync(id, model);
			if (isSucceded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Delete(string id)
		{
			var isSucceded = await _roleService.DeleteAsync(id);
			if (isSucceded) return RedirectToAction(nameof(Index));

			return NotFound();
		}



	}
}

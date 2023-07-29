using Business.Services.Abstract.Admin;
using Business.Services.Constants;
using Business.ViewModels.Admin.User;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new UserIndexVM();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!await _userManager.IsInRoleAsync(user, UserRoles.Superadmin.ToString()))
                {
                    var userWithRoles = new UserVM
                    {
                        Id = user.Id,
                        //Fullname = user.Fullname,
                        Email = user.Email,
                        Username = user.UserName,
                        Roles = roles.ToList()
                    };

                    model.Users.Add(userWithRoles);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserCreateVM()
            {
                Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.User.ToString() && r.Name != UserRoles.Superadmin.ToString()).Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateVM model)
        {
            var isSucceded = await _userService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var rolesIds = new List<string>();
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    throw new Exception("Rol movcud deyil");
                }

                rolesIds.Add(role.Id);
            }

            var model = new UserUpdateVM
            {
                Fullname = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                Roles = await _roleManager.Roles.Where(r => r.Name != UserRoles.User.ToString() && r.Name != UserRoles.Superadmin.ToString()).Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToListAsync(),

                RolesIds = rolesIds

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateVM model)
        {
            var isSucceded = await _userService.UpdateAsync(id, model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)  
        {
            var isSucceded = await _userService.DeleteAsync(id);
            if (isSucceded) { return RedirectToAction(nameof(Index));}

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await _userService.DetailsAsync(id);
            if (model != null) return View(model);

            return RedirectToAction(nameof(Index), "dashboard");
        }





    }
}

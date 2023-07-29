using Business.Services.Abstract.Admin;
using Business.Services.Constants;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Utilities
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(RoleManager<IdentityRole> roleManager, 
                                           UserManager<User> userManager, 
                                           IOurVisionRepository ourVisionRepository, 
                                           IDepartmentRepository departmentRepository,
                                           IMedicalRepository medicalRepository,
                                           IUnitOfWork unitOfWork)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedDepartmentsAsync(departmentRepository, unitOfWork);
            await SeedMedicalAsync(medicalRepository, unitOfWork);
            await SeedOurvision(ourVisionRepository, unitOfWork);
        }

        private static async Task SeedMedicalAsync(IMedicalRepository medicalRepository, IUnitOfWork unitOfWork)
        {
            var medical = await medicalRepository.GetAllAsync();
            if (medical.Count == 0)
            {
                var newMedical = new Medical()
                {
                    Title = "\r\n                            Expert physician and caring clinical staff provide you with an exceptional patient care.",
                    Description = "\r\n                            Syring Medical Center provide patients with choices to ask for the conducting and analyzing\r\n                            of several lab tests on-site at no cost for prioritized patients or at 70% for people with\r\n                            an insurance. Additional testing can be ordered off site; those costs are the responsibility\r\n                            of the enquirers.\r\n                        "
                };

                await medicalRepository.CreateAsync(newMedical);
                await unitOfWork.CommitAsync();
            }
        }

        private static async Task SeedDepartmentsAsync(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            var department = await departmentRepository.GetAllAsync();
            if (department.Count == 0)
            {
                var newDepartment = new Department()
                {
                    Title = "Medical Departments",
                    Description = "Expert physician specialists and caring clinical staff provide you\r\n                            with an exceptional patient care is what sets Syring Medical Center apaert health care\r\n                            experience."
                };

                await departmentRepository.CreateAsync(newDepartment);
                await unitOfWork.CommitAsync();
            }
        }

        private static async Task SeedOurvision(IOurVisionRepository ourVisionRepository, IUnitOfWork unitOfWork)
        {
            var ourVision = await ourVisionRepository.GetAllAsync();
            if (ourVision.Count == 0)
            {
                var newVision = new OurVision()
                {
                    Title = "Combining Quality Care & Modern Conveniences.",
                    Description = "We started One Medical with the belief that clinical excellence\r\n                            commitment to service and\r\n                            a modern approach make for a truly great experience. To bring our vision to life, we\r\n                            listened to our patients. thoughtfully applied technology, and hired the best doctors to\r\n                            create a practice that is designed specifically to meet your needs."
                };

                await ourVisionRepository.CreateAsync(newVision);
                await unitOfWork.CommitAsync();
            }
        }

        private async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in System.Enum.GetValues<UserRoles>())
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }

        private async static Task SeedUsersAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Admin");

            if (user is null)
            {
                user = new User
                {
                    UserName = "Admin",
                    FullName = "Admin",
                    Email = "admin@app.com",
                };
                var result = await userManager.CreateAsync(user, "admin123");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                await userManager.AddToRoleAsync(user, UserRoles.Superadmin.ToString());
            }
        }


    }
}

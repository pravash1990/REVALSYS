using Data;
using Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        public Functional(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IRoles roles,
           IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
        }

      

        public async Task InitAppData()
        {
            try
            {     
                await _context.CourseType.AddAsync(new CourseType { CourseName = "Default" });
                await _context.SaveChangesAsync();

                List<Learner> Learner = new List<Learner>() {
                    new Learner{Name = "Hanari Carnes", Phone = "1111111111"},
                    new Learner{Name = "HILARION-Abastos", Phone = "2222222222"}
                };
                await _context.Learner.AddRangeAsync(Learner);
                await _context.SaveChangesAsync();

            
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "Super";
                    profile.LastName = "Admin";
                    profile.Email = superAdmin.Email;
                    profile.ApplicationUserId = superAdmin.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();

                    await _roles.AddToRoles(superAdmin.Id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<string> UploadFile(List<IFormFile> files, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }

            return result;
        }

    }
}

using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Logic_Layer.Services
{
    public class UserServices
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        readonly IWebHostEnvironment _webHostEnvironment;


        public UserServices(IGenericRepository<User> genericRepository, UserManager<User> userManager,  RoleManager<IdentityRole> roleManager,IWebHostEnvironment webHostEnvironment)
        {
            _genericRepository = genericRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new User()
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                NormalizedEmail = userModel.Email
            };
            if (userModel.Image?.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = userModel.Image.FileName;
                using (FileStream fileStream = System.IO.File.Create(path + userModel.Image.FileName))
                {
                    userModel.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
                user.UserImage = "https://localhost:7241/uploads/"+fileName;
            }
            var result = await _userManager.CreateAsync(user, userModel.Password);
            await _userManager.AddToRoleAsync(user, "User");
            return result;
        }
    }
}

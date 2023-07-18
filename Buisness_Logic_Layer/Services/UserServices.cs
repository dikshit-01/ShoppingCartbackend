using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Identity;
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

        public UserServices(IGenericRepository<User> genericRepository, UserManager<User> userManager,  RoleManager<IdentityRole> roleManager)
        {
            _genericRepository = genericRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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
            var result = await _userManager.CreateAsync(user, userModel.Password);
            await _userManager.AddToRoleAsync(user, "User");
            return result;
        }
    }
}

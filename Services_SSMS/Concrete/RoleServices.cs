using Data_SSMS;
using Data_SSMS.Identity;
using Microsoft.AspNetCore.Identity;
using Services_SSMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_SSMS.Concrete
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<AppIdentityRole> _roleManager;
        private readonly UserManager<AppIdentityUser> _userManager;

        public RoleServices(RoleManager<AppIdentityRole> roleManager, UserManager<AppIdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> CreateRoleAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    return false;
                }
                var newrole = new AppIdentityRole { Name= roleName, NormalizedName=roleName.ToUpper()};
                var result = await _roleManager.CreateAsync(newrole);
                return result.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<bool> AssignRoleAsync(string userid, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.AddToRoleAsync(user,roleName);
            return result.Succeeded;
        }
    }
}

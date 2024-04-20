using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_SSMS.Abstract
{
    public interface IRoleServices
    {
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string userid, string roleName);
    }
}

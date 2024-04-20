using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.Abstract
{
    public interface IUserRepository
    {
        Task<IEnumerable<MDUsers>> GetAllUsersAsync();
        Task CreateUserAsync(MDUsers user);
        Task DeleteUserByIdAsync(string id);
        Task DeleteAllUsersAsync();
    }
}

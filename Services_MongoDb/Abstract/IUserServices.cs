using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_MongoDb.Abstract
{
    public interface IUserServices
    {
        Task<IEnumerable<MDUsers>> GetAllUsers();
        Task<MDUsers> GetUserById(string id);
        Task CreateUser(MDUsers user);
        Task DeleteUserById(string id);
        Task DeleteAllUsers();
    }
}

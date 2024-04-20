using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_SSMS.Models;

namespace Data_SSMS.Abstract
{
    public interface IUserRepositorySSMS
    {
        Task<List<SSMSUsers>> GettAllUsers();
    }
}

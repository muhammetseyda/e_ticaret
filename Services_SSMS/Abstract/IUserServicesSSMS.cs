using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_SSMS.Models;

namespace Services_SSMS.Abstract
{
    public interface IUserServicesSSMS
    {
        Task<List<SSMSUsers>> GettAllUsers();
    }
}

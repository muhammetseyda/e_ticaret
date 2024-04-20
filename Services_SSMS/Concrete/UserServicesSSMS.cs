using Services_SSMS.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_SSMS.Abstract;
using Entities_SSMS.Models;

namespace Services_SSMS.Concrete
{
    public class UserServicesSSMS : IUserServicesSSMS
    {
        private readonly IUserRepositorySSMS _userRepository;

        public UserServicesSSMS(IUserRepositorySSMS userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<SSMSUsers>> GettAllUsers()
        {
            return await _userRepository.GettAllUsers();
        }
    }
}

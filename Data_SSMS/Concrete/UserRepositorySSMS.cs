using Data_SSMS.Abstract;
using Entities_SSMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_SSMS.Concrete
{
    public class UserRepositorySSMS : IUserRepositorySSMS
    {
        private readonly AppDbContext _context;

        public UserRepositorySSMS(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SSMSUsers>> GettAllUsers() 
        {
            return await _context.Users.ToListAsync();
        }
    }
}

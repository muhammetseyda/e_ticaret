using Data_MongoDb.Abstract;
using Services_MongoDb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_MongoDb.Models;

namespace Services_MongoDb.Concrete
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<MDUsers>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<MDUsers> GetUserById(string id)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var user = users.FirstOrDefault(x => x._id == id);
            return user;
        }
        public async Task CreateUser(MDUsers user)
        {
            await _userRepository.CreateUserAsync(user);
        }
        public async Task DeleteUserById(string id)
        {
            await _userRepository.DeleteUserByIdAsync(id);
        }
        public async Task DeleteAllUsers()
        {
            await _userRepository.DeleteAllUsersAsync();
        }
    }
}

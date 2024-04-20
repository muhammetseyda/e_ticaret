using Data_MongoDb.Abstract;
using Data_MongoDb.MongoDbContext;
using Entities_MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<MDUsers> _users;
        public UserRepository(Data_MongoDb.MongoDbContext.MongoDbContext database)
        {
            _users = database.Users;
        }
        public async Task<IEnumerable<MDUsers>> GetAllUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        public async Task CreateUserAsync(MDUsers user)
        {
            await _users.InsertOneAsync(user);
        }
        public async Task DeleteUserByIdAsync(string id)
        {
            await _users.DeleteOneAsync(u => u._id == id);
        }
        public async Task DeleteAllUsersAsync()
        {
            await _users.DeleteManyAsync(FilterDefinition<MDUsers>.Empty);
        }
    }
}

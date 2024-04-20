using Entities_MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.MongoDbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<MDUsers> Users => _database.GetCollection<MDUsers>("Users");
        public IMongoCollection<MDCategories> Categories => _database.GetCollection<MDCategories>("Categories");
        public IMongoCollection<MDProducts> Products => _database.GetCollection<MDProducts>("Products");
    }
}

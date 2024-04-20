using Data_MongoDb.Abstract;
using Entities_MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.Concrete
{
    public class CategoriesRepositoryMD : ICategoriesRepositoryMD
    {
        private readonly IMongoCollection<MDCategories> _categories;
        public CategoriesRepositoryMD(Data_MongoDb.MongoDbContext.MongoDbContext database)
        {
            _categories = database.Categories;
        }

        public async Task<IEnumerable<MDCategories>> GetAllCategoriesAsync()
        {
            return await _categories.Find(name => true).ToListAsync();
        }

        public async Task<bool> CreateCategoriesAsync(MDCategories category)
        {
            await _categories.InsertOneAsync(category);
            return true;
        }
        public async Task DeleteCategoriesByIdAsync(int id)
        {
            await _categories.DeleteOneAsync(u => u.CategoryId == id);
        }
        public async Task<MDCategories> GetCategoriesByIdAsync(int id)
        {
            var cursor = await _categories.Find(x => x.CategoryId == id).ToCursorAsync();
            return await cursor.FirstOrDefaultAsync();
        }
    }
}

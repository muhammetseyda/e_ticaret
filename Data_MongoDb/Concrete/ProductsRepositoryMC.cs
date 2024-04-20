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
    public class ProductsRepositoryMC : IProductRepositoryMD
    {
        private readonly IMongoCollection<MDProducts> _products;
        public ProductsRepositoryMC(Data_MongoDb.MongoDbContext.MongoDbContext database)
        {
            _products = database.Products;
        }

        public async Task<IEnumerable<MDProducts>> GetAllProductsAsync()
        {
            return await _products.Find(productId => true).ToListAsync();
        }

        public async Task<bool> CreateProductAsync(MDProducts category)
        {
            await _products.InsertOneAsync(category);
            return true;
        }
        public async Task DeleteCategoriesByIdAsync(int id)
        {
            await _products.DeleteOneAsync(u => u.productId == id);
        }
        public async Task<MDProducts> GetProductByIdAsync(int id)
        {
            var cursor = await _products.Find(x => x.productId == id).ToCursorAsync();
            return await cursor.FirstOrDefaultAsync();
        }
    }
}

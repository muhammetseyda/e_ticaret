using Data_MongoDb.Abstract;
using Entities_MongoDb.Models;
using Services_MongoDb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_MongoDb.Concrete
{
    public class ProductsServicesMD : IProductsServicesMD
    {
        private readonly IProductRepositoryMD _productsRepositoryMD;

        public ProductsServicesMD(IProductRepositoryMD productsRepositoryMD)
        {
            _productsRepositoryMD = productsRepositoryMD;
        }

        public async Task<IEnumerable<MDProducts>> GetAllProductsAsync()
        {
            return await _productsRepositoryMD.GetAllProductsAsync();
        }

        public async Task<bool> CreateProductAsync(MDProducts category)
        {
            var result = await _productsRepositoryMD.CreateProductAsync(category);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task DeleteCategoriesByIdAsync(int id)
        {
            await _productsRepositoryMD.DeleteCategoriesByIdAsync(id);
        }

        public async Task<int> GetEndProductId()
        {
            var category = await _productsRepositoryMD.GetAllProductsAsync();
            var maxId = category.Max(x => x.productId);
            return maxId + 1;
        }

        public async Task<MDProducts> GetProductById(int id)
        {
            return await _productsRepositoryMD.GetProductByIdAsync(id);
        }
    }
}

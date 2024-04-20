using Data_MongoDb.Abstract;
using Data_MongoDb.Concrete;
using Entities_MongoDb.Models;
using Services_MongoDb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_MongoDb.Concrete
{
    public class CategoriesServicesMD : ICategoriesServicesMD
    {
        private readonly ICategoriesRepositoryMD _categoriesRepositoryMD;

        public CategoriesServicesMD(ICategoriesRepositoryMD categoriesRepositoryMD)
        {
            _categoriesRepositoryMD = categoriesRepositoryMD;
        }

        public async Task<IEnumerable<MDCategories>> GetAllCategoriesAsync()
        {
            return await _categoriesRepositoryMD.GetAllCategoriesAsync();
        }

        public async Task<bool> CreateCategoriesAsync(MDCategories category)
        {
            var result = await _categoriesRepositoryMD.CreateCategoriesAsync(category);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task DeleteCategoriesByIdAsync(int id)
        {
            await _categoriesRepositoryMD.DeleteCategoriesByIdAsync(id);
        }

        public async Task<int> GetEndCategoryId()
        {
            var category = await _categoriesRepositoryMD.GetAllCategoriesAsync();
            var maxId = category.Max(x => x.CategoryId);
            return maxId + 1;
        }

        public async Task<MDCategories> GetCategoryById(int id)
        {
            return await _categoriesRepositoryMD.GetCategoriesByIdAsync(id);
        }
    }
}

using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_MongoDb.Abstract
{
    public interface ICategoriesServicesMD
    {
        Task<IEnumerable<MDCategories>> GetAllCategoriesAsync();
        Task<bool> CreateCategoriesAsync(MDCategories category);
        Task DeleteCategoriesByIdAsync(int id);
        Task<int> GetEndCategoryId();
        Task<MDCategories> GetCategoryById(int id);
    }
}

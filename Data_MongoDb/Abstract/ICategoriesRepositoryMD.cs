using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.Abstract
{
    public interface ICategoriesRepositoryMD
    {
        Task<IEnumerable<MDCategories>> GetAllCategoriesAsync();
        Task<bool> CreateCategoriesAsync(MDCategories category);
        Task DeleteCategoriesByIdAsync(int id);
        Task<MDCategories> GetCategoriesByIdAsync(int id);
    }
}

using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_MongoDb.Abstract
{
    public interface IProductRepositoryMD
    {
        Task<IEnumerable<MDProducts>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(MDProducts category);
        Task DeleteCategoriesByIdAsync(int id);
        Task<MDProducts> GetProductByIdAsync(int id);
    }
}

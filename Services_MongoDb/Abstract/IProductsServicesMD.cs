using Entities_MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_MongoDb.Abstract
{
    public interface IProductsServicesMD
    {
        Task<IEnumerable<MDProducts>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(MDProducts category);
        Task DeleteCategoriesByIdAsync(int id);
        Task<int> GetEndProductId();
        Task<MDProducts> GetProductById(int id);
    }
}

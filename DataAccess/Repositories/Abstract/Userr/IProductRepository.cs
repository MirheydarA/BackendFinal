using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Userr
{
    public interface IProductRepository
    {
        Task<Product> GetProductWithCategory(int id);
        IQueryable<Product> FilterByName(string? name);
        Task<List<ProductCategory>> GetProductCategories(); 
    }
}

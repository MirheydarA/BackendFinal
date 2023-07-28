using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Admin
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductAndCategory(int id);
        Task<List<Product>> GetProductsWithCategory();
        Task<bool> GetProductByName(string name);
    }
}

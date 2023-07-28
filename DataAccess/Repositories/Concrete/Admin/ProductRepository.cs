using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductAndCategory(int id)
        {
            var product = await _context.Products.Include(d => d.ProductCategory).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<bool> GetProductByName(string name)
        {
            var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(d => d.Name == name);
            if (product == null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            var products = await _context.Products.Include(d => d.ProductCategory).Where(p => !p.IsDeleted).OrderByDescending(p => p.Id).ToListAsync();
            return products;
        }
    }
}

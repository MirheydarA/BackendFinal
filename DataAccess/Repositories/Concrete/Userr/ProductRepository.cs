using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Userr;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Userr
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> FilterByName(string? name)
        {
            var products = !string.IsNullOrEmpty(name) ? _context.Products.Include(d => d.ProductCategory).Where(d => d.Name.Contains(name)) : _context.Products.Include(d => d.ProductCategory).Where(d => !d.IsDeleted);
            return products;
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            return await _context.ProductCategories.Include(pc => pc.Products).Where(pc => !pc.IsDeleted).ToListAsync();
        }

        public async Task<Product> GetProductWithCategory(int id)
        {
            var product = await _context.Products.Include(d => d.ProductCategory).Where(d => !d.IsDeleted).FirstOrDefaultAsync(d => d.Id == id);
            return product;
        }
    }
}

using Business.Services.Abstract.User;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Product;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.Userr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Userr
{
    public class ProductService : IProductService
    {
        private readonly DataAccess.Repositories.Abstract.Userr.IProductRepository _productRepository;
        private readonly IPaginator _paginator;

        public ProductService(DataAccess.Repositories.Abstract.Userr.IProductRepository productRepository, IPaginator paginator )
        {
            _productRepository = productRepository;
            _paginator = paginator;
        }

        public async Task<ProductIndexVM> IndexGetAllAsync(ProductIndexVM model)
        {
            var products = await _productRepository.FilterByName(model.Name).ToListAsync();

            model = new ProductIndexVM()
            {
                Products = _paginator.GetPagedList(products, model.CurrentPage, model.PageSize),
                ProductCategories = await _productRepository.GetProductCategories()
            };
            return model;
        }
    }
}

using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.ViewModels.Product
{
    public class ProductIndexVM : PaginationVM
    {
        public ProductIndexVM()
        {
            ProductCategories = new List<ProductCategory>();
        }
        public IPagedList<Common.Entities.Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

        /////filter 
        public string? Name { get; set; }
    }
}

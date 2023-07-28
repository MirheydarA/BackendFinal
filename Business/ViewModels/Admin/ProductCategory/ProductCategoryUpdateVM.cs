using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.ProductCategory
{
    public class ProductCategoryUpdateVM
    {
        [Required, MinLength(3, ErrorMessage = "Adin uzunlugu minimum 3 simvol olmalidir")]
        public string Name { get; set; }
    }
}

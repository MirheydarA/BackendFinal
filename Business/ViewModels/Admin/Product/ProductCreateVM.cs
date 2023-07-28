using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required, MinLength(3, ErrorMessage = "Adin uzunlugu minimum 3 simvol olmalidir")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ad mutleq daxil edilmelidir")]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        [Display(Name = "Product category id")]
        public int ProductCategoryId { get; set; }
        public List<SelectListItem>? ProductCategories { get; set; }
    }
}

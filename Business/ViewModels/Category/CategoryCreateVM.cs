using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
    }
}

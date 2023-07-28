using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.FAQCategory
{
    public class FAQCategoryIndexVM
    {
        public FAQCategoryIndexVM()
        {
            FAQCategories = new List<Common.Entities.FAQCategory>();
        }
        public List<Common.Entities.FAQCategory> FAQCategories { get; set; }
    }
}

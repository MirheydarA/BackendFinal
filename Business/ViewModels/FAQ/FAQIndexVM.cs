using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.FAQCategory
{
    public class FAQIndexVM
    {
        public FAQIndexVM()
        {
            List<Common.Entities.FAQCategory> fAQCategories = new List<Common.Entities.FAQCategory>();
            List<Question> Questions = new List<Question>();
        }
        public List<Common.Entities.FAQCategory> fAQCategories { get; set; }
        public List<Common.Entities.Question> Questions { get; set; }
        
    }
}

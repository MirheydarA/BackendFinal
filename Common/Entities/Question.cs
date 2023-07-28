using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public string Answer { get; set; }
        public FAQCategory FAQCategory { get; set; }
        public int FAQCategoryId { get; set; }
    }
}

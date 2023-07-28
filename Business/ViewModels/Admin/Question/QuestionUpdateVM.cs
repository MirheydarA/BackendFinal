using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Question
{
    public class QuestionUpdateVM
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public int FAQCategoryId { get; set; }
        public List<SelectListItem>? FAQCategories { get; set; }
    }
}

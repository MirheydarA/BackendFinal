using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Question
{
    public class QuestionIndexVM
    {
        public QuestionIndexVM()
        {
            Questions = new List<Common.Entities.Question>();
        }
        public List<Common.Entities.Question> Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.WhyChoose
{
    public class WhyChooseIndexVM
    {
        public WhyChooseIndexVM()
        {
            WhyChooses = new List<Common.Entities.WhyChoose>();
        }
        public List<Common.Entities.WhyChoose> WhyChooses { get; set; }
    }
}

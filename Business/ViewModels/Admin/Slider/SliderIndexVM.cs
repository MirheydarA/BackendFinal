using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderIndexVM
    {
        public SliderIndexVM()
        {
            Sliders = new List<Common.Entities.Slider>();
        }
        public ICollection<Common.Entities.Slider> Sliders { get; set; }
    }
}

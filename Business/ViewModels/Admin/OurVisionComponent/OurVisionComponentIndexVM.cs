using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OurVisionComponent
{
    public class OurVisionComponentIndexVM
    {
        public OurVisionComponentIndexVM()
        {
            OurVisionComponents = new List<Common.Entities.OurVisionComponent>();
        }
        public ICollection<Common.Entities.OurVisionComponent> OurVisionComponents { get; set; }
    }
}

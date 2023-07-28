using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OurVision
{
    public class OurVisionIndexVM
    {
        public OurVisionIndexVM()
        {
            OurVisions = new List<Common.Entities.OurVision>();
        }
        public List<Common.Entities.OurVision> OurVisions{ get; set; }
    }
}

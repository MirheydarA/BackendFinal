using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OurVision
{
    public class OurVisionCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OurVisionComponentId { get; set; }
        public List<SelectListItem> OurVisionComponents { get; set; }
    }
}


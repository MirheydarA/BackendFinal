using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUs
{
    public class AboutUsIndexVM
    {
        public AboutUsIndexVM()
        {
            AboutUss = new List<Common.Entities.AboutUs>();
        }
        public ICollection<Common.Entities.AboutUs> AboutUss { get; set; }
    }
}

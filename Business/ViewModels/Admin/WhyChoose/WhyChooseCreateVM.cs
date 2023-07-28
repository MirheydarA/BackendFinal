using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.WhyChoose
{
    public class WhyChooseCreateVM
    {
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}

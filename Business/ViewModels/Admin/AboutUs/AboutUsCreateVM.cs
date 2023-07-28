using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUs
{
    public class AboutUsCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile MainPhotoName { get; set; }
        public IFormFile Photoname { get; set; }
    }
}

using Business.Services.Utilities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Video
{
    public class VideoCreateVM
    {
        public IFormFile CoverPhoto { get; set; }
        public string Link { get; set; }
    }
}

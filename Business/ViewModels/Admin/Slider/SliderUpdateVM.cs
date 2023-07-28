using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderUpdateVM
    {
        [MinLength(3, ErrorMessage = "Başlığın ölçüsü 3 simvoldan böyük olmalıdır")]
        public string Title { get; set; }
        [MinLength(3, ErrorMessage = "Başlığın ölçüsü 3 simvoldan böyük olmalıdır")]
        public string Subtitle { get; set; }
        public IFormFile? Photoname { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Başlıq mütləq daxil edilməlidir")]
        [MinLength(3, ErrorMessage = "Başlığın ölçüsü 3 simvoldan böyük olmalıdır")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Başlıq mütləq daxil edilməlidir")]
        [MinLength(3, ErrorMessage = "Başlığın ölçüsü 3 simvoldan böyük olmalıdır")]
        public string Subtitle { get; set; }
        [Required(ErrorMessage = "Şəkil mütləq daxil edilməlidir")]
        public IFormFile Photoname { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Doctor
{
    public class DoctorCreateVM
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Uzunluq maksimum 20 simvol olmalidir")]
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Uzunluq maksimum 50 simvol olmalidir")]
        public string Qualification { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Uzunluq maksimum 20 simvol olmalidir")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Uzunluq maksimum 20 simvol olmalidir")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Uzunluq maksimum 50 simvol olmalidir")]
        public string WorkingTime { get; set; }
        [Required]
        [MaxLength(5000, ErrorMessage = "Uzunluq maksimum 5000 simvol olmalidir")]
        public string Description { get; set; }
        [Required]
        public int DutyId { get; set; }
        public List<SelectListItem>? Duties { get; set; }
    }
}

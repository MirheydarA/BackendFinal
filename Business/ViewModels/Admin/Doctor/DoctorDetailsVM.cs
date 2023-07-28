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
    public class DoctorDetailsVM
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Qualification { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WorkingTime { get; set; }
        public string Description { get; set; }
        public Common.Entities.Duty Duty { get; set; }
    }
}

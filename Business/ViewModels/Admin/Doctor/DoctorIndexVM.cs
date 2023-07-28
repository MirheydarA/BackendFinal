using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Doctor
{
    public class DoctorIndexVM
    {
        public DoctorIndexVM()
        {
            Doctors = new List<Common.Entities.Doctor>();
        }
        public List<Common.Entities.Doctor> Doctors { get; set; }
    }
}

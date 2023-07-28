using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.ViewModels.Doctor
{
    public class DoctorIndexVM : PaginationVM
    {
        public DoctorIndexVM()
        {
            Duties = new List<Duty>();
        }
        public IPagedList<Common.Entities.Doctor> Doctors { get; set; }
        public List<Duty> Duties { get; set; }


        //Filter Properties//
        public string? Name { get; set; }
    }
}

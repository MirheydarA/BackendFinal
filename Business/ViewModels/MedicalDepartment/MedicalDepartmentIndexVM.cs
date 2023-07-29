using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.MedicalDepartment
{
    public class MedicalDepartmentIndexVM
    {
        public List<Medical> Medicals { get; set; }
        public List<DepartmentComponent> DepartmentComponents { get; set; }

    }
}

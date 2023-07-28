using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.DepartmentComponent
{
    public class DepartmentComponentIndexVM
    {
        public DepartmentComponentIndexVM()
        {
            DepartmentComponents = new List<Common.Entities.DepartmentComponent>();
        }
        public List<Common.Entities.DepartmentComponent> DepartmentComponents { get; set; }
    }
}

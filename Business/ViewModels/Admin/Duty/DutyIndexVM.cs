using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Duty
{
    public class DutyIndexVM
    {
        public DutyIndexVM()
        {
            Duties = new List<Common.Entities.Duty>();
        }
        public List<Common.Entities.Duty> Duties { get; set; }
    }
}

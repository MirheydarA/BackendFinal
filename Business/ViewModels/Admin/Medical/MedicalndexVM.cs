using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Medical
{
    public class MedicalndexVM
    {
        public MedicalndexVM()
        {
            Medicals = new List<Common.Entities.Medical>();
        }
        public List<Common.Entities.Medical> Medicals { get; set; }
    }
}

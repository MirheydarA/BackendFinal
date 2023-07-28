using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Photoname { get; set; }
        public Duty Duty { get; set; }
        public int DutyId { get; set; }
        public string Qualification { get; set;}
        public string PhoneNumber { get; set; }
        public string Email { get; set;}
        public string WorkingTime { get; set;}
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Common.Entities.Base;

namespace Common.Entities
{
    public class Department : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

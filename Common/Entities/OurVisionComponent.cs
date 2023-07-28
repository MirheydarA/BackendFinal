using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class OurVisionComponent : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photoname { get; set; }
    }
}

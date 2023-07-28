using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities.Base;

namespace Common.Entities
{
    public class Video : BaseEntity
    {
        public string CoverPhoto { get; set; }
        public string Link { get; set; }
    }
}

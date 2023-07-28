using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Video
{
    public class VideoIndexVM
    {
        public VideoIndexVM()
        {
            Videos = new List<Common.Entities.Video>();
        }
        public List<Common.Entities.Video> Videos { get; set; }
    }
}

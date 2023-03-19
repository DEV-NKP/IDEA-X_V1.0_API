using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class GeneralPostEditModel : GeneralPostModel
    {
        public int POST_IDR { get; set; }
        public string TIMELINE_TEXTR { get; set; }
        public string POST_TAGR { get; set; } = null;
        public byte[] TIMELINE_IMGRC { get; set; } = null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class GeneralPostModelWithTimeAndIP : GeneralPostModel
    {
        public string POST_IP { get; set; }
        public string POSTING_TIME { get; set; }
        public string USERNAME { get; set; }
    }
}

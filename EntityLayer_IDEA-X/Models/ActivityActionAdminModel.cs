using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class ActivityActionAdminModel
    {

        public String USERNAME { get; set; }
        public int LIKE { get; set; }
        public int DISLIKE { get; set; }

        public int LIKEtake { get; set; }
        public int DISLIKEtake { get; set; }
    }
}

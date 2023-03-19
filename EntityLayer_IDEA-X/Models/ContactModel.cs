using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class ContactModel : ContactRegModel
    {
        
        public int CONTACT_ID { get; set; }
        public string LEVEL { get; set; }
        public string STATUS { get; set; }
        public string LOGIN_TIME { get; set; }
        public string LOGIN_IP { get; set; }

    }
}

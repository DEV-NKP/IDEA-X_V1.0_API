using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class MessageRequestModel
    {

        public int REQUEST_ID { get; set; }
        public string SENDER { get; set; }
        public string RECEIVER { get; set; }
        public string MESSAGE_TIME { get; set; }
    }
}

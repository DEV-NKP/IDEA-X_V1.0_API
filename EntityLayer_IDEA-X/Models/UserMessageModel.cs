using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class UserMessageModel
    {
        public int MESSAGE_ID { get; set; }
        [Required]
        public string SESSION_NAME { get; set; }
        [Required]
        public string SENDER { get; set; }
        [Required]
        public string RECEIVER { get; set; }
        [Required]
        public string USER_MESSAGE { get; set; }
        public string MESSAGE_TIME { get; set; }
    }
}

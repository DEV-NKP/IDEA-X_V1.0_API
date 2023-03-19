using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class ChatBoxModel
    {
        public string CHAT_SESSION { get; set; }
        [Required]
        public string SENDER { get; set; }
        [Required]
        public string RECEIVER { get; set; }
        public string CHAT_TIME { get; set; }
    }
}

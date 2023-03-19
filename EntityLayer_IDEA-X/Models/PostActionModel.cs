using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class PostActionModel
    {
       
        public int POST_ACTION_ID { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public Nullable<int> POST_ID { get; set; }
        public string POST_STATUS { get; set; }
    }
}

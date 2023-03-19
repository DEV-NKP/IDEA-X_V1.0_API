using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class ContactRegModel
    {
        public string FIRST_NAME { get; set; }

        public string LAST_NAME { get; set; }
        [Required]
        public string EMAIL { get; set; }
        [Required]
        public string MESSAGE { get; set; }

        public string USERNAME { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class UpdatePassModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string o_pass { get; set; }
        [Required]
        public string n_pass { get; set; }
    }
}

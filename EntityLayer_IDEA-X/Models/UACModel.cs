using EntityLayer_IDEA_X.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class UACModel
    {

        [Required]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required]
        public string USERNAME { get; set; }

        [Required]
        [PasswordValidation]
        public string PASSWORD { get; set; }
    }
}

using EntityLayer_IDEA_X.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class LoginAuthModel
    {
        [Required]
        public string USERNAME { get; set; }
        [Required]
        [PasswordValidation]
        public string PASSWORD { get; set; }
        
        [EmailAddress]
        public string EMAIL { get; set; }
        public string TOKEN_KEY { get; set; }
        public string EXPIRE_TIME { get; set; }
    }
}

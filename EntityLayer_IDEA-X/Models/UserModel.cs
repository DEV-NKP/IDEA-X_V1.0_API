using EntityLayer_IDEA_X.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class UserModel : UserCredentialsModel
    {
        [Required]
        public string USERNAME { get; set; }

        [Required]
        [PasswordValidation]
        public string PASSWORD { get; set; }

        [Required]
        public string COUNTRY { get; set; }

        public string INDUSTRY { get; set; }
        [Required]
        public string EDUCATIONAL_INSTITUTION { get; set; }
        [Required]
        public string DEPARTMENT { get; set; }
        [Required]
        [RegularExpression("^(\\+\\d{1,3}( )?)?((\\(\\d{1,3}\\))|\\d{1,3})[- .]?\\d{3,4}[- .]?\\d{4}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string MOBILE { get; set; }

        [Required]
        public string HEADLINE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EntityLayer_IDEA_X.Models
{
    public class UserCredentialsModel
    {
        [Required]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required]
        public string FIRST_NAME { get; set; }
        [Required]
        public string LAST_NAME { get; set; }
        [Required]
        public string DATE_OF_BIRTH { get; set; }
        [Required]
        public string GENDER { get; set; }

       
        public byte[] PROFILE_IMG { get; set; }
    }
}

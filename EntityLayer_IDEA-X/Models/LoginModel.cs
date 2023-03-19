using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer_IDEA_X.Models
{
    public class LoginModel : LoginAuthModel
    {
        public int LOGIN_ID { get; set; }
        public string LOGIN_TIME { get; set; }
        public string LOGIN_IP { get; set; }
        public string USER_LEVEL { get; set; }

    }
}

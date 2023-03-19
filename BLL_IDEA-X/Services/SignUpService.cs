using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class SignUpService
    {
        public static bool SignUp(UserModel u)
        {
            var output = false;
            if(u != null)
            {
               output=  AllUserService.AddUserCredentials(u, "USER");
                output = UserDetailsService.AddUserDetails(u);
                return output;
            }
            return output;

        }
    }
}

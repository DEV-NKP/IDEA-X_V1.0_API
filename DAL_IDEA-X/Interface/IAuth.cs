using DAL_IDEA_X.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Interface
{
    public interface IAuth
    {
        LOGIN CreateAuth(string uname, string pass, LOGIN auth);
        LOGIN IsAuthenticated(string token);
        bool Logout(string authkey,LOGIN exp);
    }
}

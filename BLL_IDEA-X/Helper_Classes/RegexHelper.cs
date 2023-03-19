using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Helper_Classes
{
    public class RegexHelper
    {
        readonly static string mail_pattern = @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        
        public static bool CheckMailRegex(string mail)
        {
            var regex = new Regex(mail_pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(mail);
        }
    }
}

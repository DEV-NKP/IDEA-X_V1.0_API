using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Helper_Classes
{
    public class TokenGenerator
    {
        public static string GenerateNewTokenKey()
        {
            var guid = Guid.NewGuid().ToString();
            using (SHA256 sHA256 = SHA256.Create())
            {
                byte[] e_pass = sHA256.ComputeHash(Encoding.UTF8.GetBytes(guid));
                StringBuilder output = new StringBuilder();
                for (int i = 0; i < e_pass.Length; i++)
                {
                    output.Append(String.Format("{0:x2}", e_pass[i]));
                }
                return output.ToString();
            }
        }
    }
}

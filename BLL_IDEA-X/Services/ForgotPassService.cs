using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class ForgotPassService
    {
        public static bool ForgotPassRequest(AllUserModel model)
        {
            var response = AllUserService.GetUserByEmail(model.EMAIL);
            if (response != null)
            {
                UpdatePassModel updatePass = new UpdatePassModel();
                updatePass.n_pass = model.PASSWORD;
                updatePass.o_pass = response.PASSWORD;
                updatePass.username = response.USERNAME;
                var res = AllUserService.UpdateUserPassword(updatePass);
                return res;

            }
            return false;

        }
    }
}

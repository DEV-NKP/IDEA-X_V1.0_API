using BLL_IDEA_X.Helper_Classes;
using DAL_IDEA_X;
using DAL_IDEA_X.EntityFramework;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class AdminService
    {
        //Update -- dipanwita
        public static bool ChangeAdminPass(UpdatePassModel model)
        {
            var res = false;
            var data = AllUserService.GetUserByName(model.username);
            if (data != null)
            {
                string prev_e_pass = EncryptionAndHashLogic.HashPassword(model.o_pass);
                string e_pass = EncryptionAndHashLogic.HashPassword(model.n_pass);
                if (data.PASSWORD.Equals(prev_e_pass))
                {
                    var admin_model = new AdminModel()
                    {
                        EMAIL = data.EMAIL,
                        USERNAME = data.USERNAME,
                        USER_PASSWORD = e_pass
                    };

                    
                    ADMIN admin = new ADMIN();
                    var output = MappingService.MapClass(admin_model, admin);
                    
                    res = AllUserService.UpdateUserPassword(model);
                    res = DataAcessFactory.AdminDataAccess().Update(output);
                    return res;
                }
                return res;
            }
            return res;
        }

        //Create -- dipanwita
        public static bool AddAdminInfo(AdminModel model)
        {
            var output = false;
            if(model != null)
            {
                var user = new UserModel()
                {
                    PASSWORD = model.USER_PASSWORD,
                    USERNAME = model.USERNAME,
                    EMAIL = model.EMAIL,
                };
                output = AllUserService.AddUserCredentials(user, "ADMIN");

                ADMIN admin = new ADMIN();
                var res = MappingService.MapClass(model, admin);
                output = DataAcessFactory.AdminDataAccess().Create(res);
            }
            return output;


        }

        //Delete -- dipanwita

        public static bool RemoveAdminInfo(string uname)
        {
            if(uname != null)
            {
                var data = GetAdminInfo(uname);
                if(data != null)
                {
                    return DataAcessFactory.AdminDataAccess().Delete(uname);
                }
                return false;
            }
            return false;
        }

        //Read -- dipanwita
        public static AdminModel GetAdminInfo(string uname)
        {
            var data = DataAcessFactory.AdminDataAccess().Get(uname);
            AdminModel model = new AdminModel();
            var output = MappingService.MapClass(data, model);
            return output;
        }

        public static List<AdminModel> GetAdminProfile()
        {
            var data = DataAcessFactory.AdminDataAccess().Get();
            List<AdminModel> output = new List<AdminModel>();
            foreach (var item in data)
            {
                AdminModel model = new AdminModel();
                output.Add(MappingService.MapClass(item, model));
            }
            return output;
        }

    }
}

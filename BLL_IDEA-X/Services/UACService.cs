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
    public class UACService
    {
        //Create -- dipanwita
        public static bool AddUserController(UACModel model)
        {
            var output = false;

            if(model != null)
            {
                UserModel u = new UserModel()
                {
                    USERNAME = model.USERNAME,
                    PASSWORD = model.PASSWORD,
                    EMAIL = model.EMAIL
                };
                
                output = AllUserService.AddUserCredentials(u, "UAC");

                USER_ACCESS_CONTROLLER uac = new USER_ACCESS_CONTROLLER(

                )
                {
                    EMAIL = model.EMAIL,
                    USER_PASSWORD = model.PASSWORD,
                    USERNAME = model.USERNAME,
                };
                uac.USER_PASSWORD = EncryptionAndHashLogic.HashPassword(uac.USER_PASSWORD);
                output = DataAcessFactory.UACDataAccess().Create(uac);
            }
            return output;
        }

        //Read -- dipanwita
        public  static List<UACModel> GetUserControllerList()
        {
            var data = DataAcessFactory.UACDataAccess().Get();
            var users = (from u_d in data
                         orderby u_d.USERNAME descending
                         select u_d).ToList();
            List<UACModel> models = new List<UACModel>();
            foreach (var item in users)
            {
                models.Add(new UACModel()
                {
                    EMAIL = item.EMAIL,
                    PASSWORD = item.USER_PASSWORD,
                    USERNAME = item.USERNAME,
                });
            }
            return models;
        }
        public static List<UACModel> SearchUserController(string text)
        {
            var data = GetUserControllerList();
            var users = (from u_d in data
                         where u_d.USERNAME.Contains(text)
                         || u_d.EMAIL.Contains(text)
                         select u_d).ToList();
            List<UACModel> models = new List<UACModel>();
            foreach (var item in users)
            {
                models.Add(new UACModel()
                {
                    EMAIL = item.EMAIL,
                    PASSWORD = item.PASSWORD,
                    USERNAME = item.USERNAME,
                });
            }
            return models;
        }
        //Update -- dipanwita
        public static bool ChangeUACPass(UpdatePassModel model)
        {
            var res = false;
            var data = AllUserService.GetUserByName(model.username);
            if (data != null)
            {
                string prev_e_pass = EncryptionAndHashLogic.HashPassword(model.o_pass);
                string e_pass = EncryptionAndHashLogic.HashPassword(model.n_pass);
                if (data.PASSWORD.Equals(prev_e_pass))
                {



                    USER_ACCESS_CONTROLLER output = new USER_ACCESS_CONTROLLER()
                    {
                        EMAIL = data.EMAIL,
                        USERNAME = data.USERNAME,
                        USER_PASSWORD = e_pass,
                    };
                    res = AllUserService.UpdateUserPassword(model);
                    res = DataAcessFactory.UACDataAccess().Update(output);
                    return res;
                }
                return res;
            }
            return res;
        }
        //Delete -- dipanwita
        public static bool RemoveUserController(string uname)
        {
            var output = false;
           if(uname != null)
            {

                var user = AllUserService.GetUserByName(uname);
                if(user != null)
                {
                    output = LoginService.RemoveLoginInfo(uname);
                    output = AllUserService.RemoveUserInfo(uname);
                    output = DataAcessFactory.UACDataAccess().Delete(uname);
                    return output;
                }
                return output;
            }
            return output;
        }
        //Read -- dipanwita
        public static UACModel GetUserControllerInfoByName(string uname)
        {
            var data = GetUserControllerList();
            var uac = data.Where(d => d.USERNAME.Equals(uname)).SingleOrDefault();
            return uac;
        }
    }
}

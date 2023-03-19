using BLL_IDEA_X.Helper_Classes;
using DAL_IDEA_X;
using DAL_IDEA_X.EntityFramework;
using EntityLayer_IDEA_X;
using EntityLayer_IDEA_X.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class AllUserService
    {
        public static bool VerifyUserPass(AllUserModel model)
        {
            var user = GetUserByName(model.USERNAME);
            if (user != null)
            {
                var prev_pass = EncryptionAndHashLogic.HashPassword(model.PASSWORD);
                if (user.PASSWORD.Equals(prev_pass)) return true;
                return false;
            }
            return false;
        }


        //Read --anindra
        public static List<AllUserModel> GetAllUser()
        {
            var lst = DataAcessFactory.AllUsersDataAccess().Get();
            var users = (from u_d in lst
                         orderby u_d.USERNAME ascending
                         select u_d).ToList();
            var output = new List<AllUserModel>();
            AllUserModel model = new AllUserModel();
            foreach (var item in users)
            {
                var a_u = MappingService.MapClass(item, model);
                output.Add(a_u);
            }
            return output;
        }
        //Read -- anindra
        public static AllUserModel GetUserByName(string uname)
        {
            ALL_USERS data = DataAcessFactory.AllUsersDataAccess().Get(uname);
            
            AllUserModel model = new AllUserModel();
            AllUserModel output = MappingService.MapClass(data, model);
            return output;
        }

        public static List<AllUserModel> SearchUserList(string text)
        {
            var lst = GetAllUser();
            var users = (from u_d in lst
                         where u_d.USERNAME.Contains(text)
                         || u_d.EMAIL.Contains(text)
                         || u_d.LEVEL.Contains(text)
                         select u_d).ToList();
            var output = new List<AllUserModel>();
            AllUserModel model = new AllUserModel();
            foreach (var item in users)
            {
                var a_u = MappingService.MapClass(item, model);
                output.Add(a_u);
            }
            return output;
        }
        //Create -- Anindra
        public static bool AddUserCredentials(UserModel details,string level)
        {
            var model = new AllUserModel
            {
                EMAIL = details.EMAIL,
                USERNAME = details.USERNAME,
                PASSWORD = details.PASSWORD,
                LEVEL = level,
            };

            model.PASSWORD = EncryptionAndHashLogic.HashPassword(model.PASSWORD);
            ALL_USERS a_u = new ALL_USERS();
            var u_d = MappingService.MapClass(model, a_u);
            return DataAcessFactory.AllUsersDataAccess().Create(u_d);
        }
        //Read -- anindra
        public static AllUserModel GetUserByUserNameAndPassword(string uname,string pass)
        {
            var data = DataAcessFactory.AllUsersDataAccess().Get();
            var user = data.Where(u => u.USERNAME.Equals(uname)
            && u.PASSWORD.Equals(pass)).SingleOrDefault();
            if(user != null)
            {
                AllUserModel model = new AllUserModel();
                var output = MappingService.MapClass(user, model);
                return output;
            }
            return null;
        }
        //Read -- anindra
        public static AllUserModel GetUserByEmail(string mail)
        {
            ALL_USERS data = DataAcessFactory.AllUsersDataAccess().Get(mail);
            if (data != null)
            {
                AllUserModel model = new AllUserModel();
                AllUserModel output = MappingService.MapClass(data, model);
                return output;
            }
            return null;
        }

        public static bool UserExist(string mail)
        {
            ALL_USERS data = DataAcessFactory.AllUsersDataAccess().Get(mail);
            if (data != null)
            {
                return true;
            }
            return false;
        }
        //Read -- anindra
        public static AllUserModel GetUserByLevel(string uname,string email)
        {
            var data = DataAcessFactory.AllUsersDataAccess().Get();
            var user = data.Where(u => u.USERNAME.Equals(uname) && u.EMAIL.Equals(email)
            && (u.LEVEL.Equals("USER") || u.LEVEL.Equals("BANNED"))).SingleOrDefault();
            AllUserModel allUserModels = new AllUserModel();
            var output = MappingService.MapClass(user, allUserModels);
            return output;
        }

        //Delete --anindra
        public static bool RemoveUserInfo(string uname)
        {
            if(uname != null)
            {
                return DataAcessFactory.AllUsersDataAccess().Delete(uname);
            }
            return false;
        }
        //Update -- anindra
        public static bool UpdateUserPassword(UpdatePassModel passModel)
        {
            var user = GetUserByName(passModel.username);
            if(user != null)
            {
                if (user.PASSWORD.Equals(EncryptionAndHashLogic.HashPassword(passModel.o_pass)))
                {
                    AllUserModel model = new AllUserModel
                    {
                        EMAIL = user.EMAIL,
                        LEVEL = user.LEVEL,
                        PASSWORD = EncryptionAndHashLogic.HashPassword(passModel.n_pass),
                        USERNAME = user.USERNAME,
                    };
                    ALL_USERS a_u = new ALL_USERS();
                    var u_d = MappingService.MapClass(model, a_u);
                    return DataAcessFactory.AllUsersDataAccess().Update(u_d);
                }
                return false;
            }
            return false;

        }

        public static bool UpdateUserLevel(string uname,string level)
        {
            var user = GetUserByName(uname);
            if (user != null)
            {
                
                    AllUserModel model = new AllUserModel
                    {
                        EMAIL = user.EMAIL,
                        LEVEL =level,
                        PASSWORD = user.PASSWORD,
                        USERNAME = user.USERNAME,
                    };
                    ALL_USERS a_u = new ALL_USERS();
                    var u_d = MappingService.MapClass(model, a_u);
                    return DataAcessFactory.AllUsersDataAccess().Update(u_d);
                
            }
            return false;

        }
    }
}

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
    public class LoginService
    {
        //Create -- anindra
        public static LoginModel Authenticate(LoginAuthModel model)
        {
            var user = AllUserService.GetUserByName(model.USERNAME);
            if(user != null)
            {

                LOGIN login = new LOGIN()
                {
                    USERNAME = user.USERNAME,
                    LOGIN_TIME = IDEAX_Functions.time(),
                    LOGIN_IP = IDEAX_Functions.ip(),
                    EMAIL = user.EMAIL,
                    EXPIRE_TIME = null,
                    TOKEN_KEY = TokenGenerator.GenerateNewTokenKey(),
                    USER_LEVEL = user.LEVEL
                };
                var pass = EncryptionAndHashLogic.HashPassword(model.PASSWORD);
                var res = DataAcessFactory.AuthDataAcess().CreateAuth(model.USERNAME,
                    pass,
                    login);

                if (res != null)
                {
                    LoginModel output = new LoginModel()
                    {
                        EMAIL = login.EMAIL,
                        LOGIN_TIME = login.LOGIN_TIME,
                        LOGIN_IP = login.LOGIN_IP,
                        USERNAME = user.USERNAME,
                        TOKEN_KEY = res.TOKEN_KEY,
                        EXPIRE_TIME = login.EXPIRE_TIME,
                        USER_LEVEL = login.USER_LEVEL,
                        PASSWORD = pass
                    };
                    return output;
                }
                return null;
            }
            return null;

        }

        //Read -- Anindra
        public static LoginModel CheckAuthToken(string key)
        {
            var data = DataAcessFactory.AuthDataAcess().IsAuthenticated(key);
            if (data != null)
            {
                LoginModel model = new LoginModel()
                {
                    EMAIL = data.EMAIL,
                    USERNAME = data.USERNAME,
                    LOGIN_IP = data.LOGIN_IP,
                    TOKEN_KEY = data.TOKEN_KEY,
                    LOGIN_TIME = data.LOGIN_TIME,
                    EXPIRE_TIME = data.EXPIRE_TIME,
                    USER_LEVEL = data.USER_LEVEL,
                    LOGIN_ID = data.LOGIN_ID
                };
                return model;
            }
            return null;
        }

        //Update -- Anindra
        public static bool ExpireAuth(string authkey)
        {
            if(authkey != null)
            {
                var data = CheckAuthToken(authkey);
                if (data != null)
                {
                    LOGIN login = new LOGIN()
                    {
                        USERNAME = data.USERNAME,
                        LOGIN_TIME = data.LOGIN_TIME,
                        LOGIN_IP = data.LOGIN_IP,
                        EMAIL = data.EMAIL,
                        EXPIRE_TIME = IDEAX_Functions.time(),
                        TOKEN_KEY = data.TOKEN_KEY,
                        USER_LEVEL = data.USER_LEVEL,
                        LOGIN_ID = data.LOGIN_ID
                    };
                    return DataAcessFactory.AuthDataAcess().Logout(authkey, login);
                }
                return false;
            }
            return false;
        }

        //Read -- Anindra
        public static List<LoginModel> GetUserLoginInfo()
        {
            List<LoginModel> models = new List<LoginModel>();
            var data = DataAcessFactory.LoginDataAccess().Get();
            var users = (from u_d in data
                         orderby u_d.LOGIN_ID descending
                         select u_d).ToList();
            foreach (var item in users)
            {
                models.Add(new LoginModel()
                {
                    LOGIN_ID = item.LOGIN_ID,
                    EMAIL = item.EMAIL,
                    EXPIRE_TIME = item.EXPIRE_TIME,
                    LOGIN_IP = item.LOGIN_IP,
                    LOGIN_TIME = item.LOGIN_TIME,
                    TOKEN_KEY = item.TOKEN_KEY,
                    USERNAME = item.USERNAME,
                    USER_LEVEL = item.USER_LEVEL,
                    
                });

            }
            return models;
        }

        //Delete -- Anindra
        public static bool RemoveLoginInfo(string username)
        {
            var output = false;
            if(username != null)
            {
                var data = DataAcessFactory.LoginDataAccess().Get();
                var list = data.Where(d => d.USERNAME.Equals(username)
                   && d.EXPIRE_TIME != null).ToList();
                foreach (var item in list)
                {
                    LOGIN l = new LOGIN()
                    {
                        EMAIL = item.EMAIL,
                        EXPIRE_TIME = item.EXPIRE_TIME,
                        LOGIN_ID = item.LOGIN_ID,
                        LOGIN_IP = item.LOGIN_IP,
                        LOGIN_TIME = item.LOGIN_TIME,
                        TOKEN_KEY = item.TOKEN_KEY,
                        USERNAME = item.USERNAME,
                        USER_LEVEL = item.USER_LEVEL,
                    };
                    output = DataAcessFactory.LoginDataAccess()
                        .Delete(l);


                }
            }

            return output;

        }
        public static List<LoginModel> GetUserLoginInfoList(string name)
        {
            List<LoginModel> models = new List<LoginModel>();
            var data = DataAcessFactory.LoginDataAccess().Get();
            data = data.Where(u => u.USERNAME.Equals(name)).ToList();
            foreach (var item in data)
            {
                models.Add(new LoginModel()
                {
                    EMAIL = item.EMAIL,
                    EXPIRE_TIME = item.EXPIRE_TIME,
                    LOGIN_IP = item.LOGIN_IP,
                    LOGIN_TIME = item.LOGIN_TIME,
                    TOKEN_KEY = item.TOKEN_KEY,
                    USERNAME = item.USERNAME,
                    USER_LEVEL = item.USER_LEVEL,
                    LOGIN_ID = item.LOGIN_ID

                });

            }
            return models;
        }

        public static bool UpdateUserLoginLevel(string uname,string level)
        {
            var logins = GetUserLoginInfoList(uname);
            if(logins != null)
            {
                var data = logins.Where(u => u.EXPIRE_TIME == null).SingleOrDefault();
                if (data != null)
                {
                    LOGIN login = new LOGIN()
                    {
                        USERNAME = data.USERNAME,
                        LOGIN_TIME = data.LOGIN_TIME,
                        LOGIN_IP = data.LOGIN_IP,
                        EMAIL = data.EMAIL,
                        EXPIRE_TIME = data.EXPIRE_TIME,
                        TOKEN_KEY = data.TOKEN_KEY,
                        USER_LEVEL = level,
                        LOGIN_ID = data.LOGIN_ID
                    };
                    return DataAcessFactory.LoginDataAccess().Update(login);
                }
                return false;
            }
            return false;
        }

        public static List<LoginModel> SearchLoginInfoList(string text)
        {
            List<LoginModel> models = new List<LoginModel>();
            var data = DataAcessFactory.LoginDataAccess().Get();
            var users = (from u_d in data
                         where u_d.LOGIN_ID.ToString().Contains(text)
                         || u_d.USERNAME.Contains(text)
                         || u_d.EMAIL.Contains(text)
                         || u_d.LOGIN_TIME.Contains(text)
                         || u_d.LOGIN_IP.Contains(text)
                         orderby u_d.LOGIN_ID descending
                         select u_d).ToList();
            foreach (var item in users)
            {
                models.Add(new LoginModel()
                {
                    EMAIL = item.EMAIL,
                    EXPIRE_TIME = item.EXPIRE_TIME,
                    LOGIN_IP = item.LOGIN_IP,
                    LOGIN_TIME = item.LOGIN_TIME,
                    TOKEN_KEY = item.TOKEN_KEY,
                    USERNAME = item.USERNAME,
                    USER_LEVEL = item.USER_LEVEL,
                    LOGIN_ID = item.LOGIN_ID
                });

            }
            return models;
        }
    }
}

using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class LoginRepo : IRepo<LOGIN, int, string>,IAuth
    {
        IDEA_XEntities db;
        public LoginRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(LOGIN data)
        {
            db.LOGINS.Add(data);
            return db.SaveChanges() > 0;
        }

        public LOGIN CreateAuth(string uname,string pass,LOGIN auth)
        {
            var user = db.ALL_USERS.Where(u => u.USERNAME.Equals(uname) && u.PASSWORD.Equals(pass))
                .FirstOrDefault();
            if(user != null)
            {
                var a = db.LOGINS.Where(u => u.USERNAME.Equals(user.USERNAME) 
                && u.EXPIRE_TIME == null)
                    .SingleOrDefault();
                if(a == null)
                {
                    db.LOGINS.Add(auth);
                    db.SaveChanges();
                    return auth;
                }
                if (a != null && a.EXPIRE_TIME != null)
                {
                    db.LOGINS.Add(auth);
                    db.SaveChanges();
                    return auth;
                }
                
                return a;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.LOGINS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.LOGINS.Remove(data);
            return db.SaveChanges() > 0;

        }
  

        public bool Delete(LOGIN data)
        {
            var r_data = Get(data.LOGIN_ID);
            db.LOGINS.Remove(r_data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.LOGINS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<LOGIN> Get()
        {
            return db.LOGINS.ToList();
        }

        public LOGIN Get(int id)
        {
            return db.LOGINS.Find(id);
        }

        public LOGIN Get(int id, string name)
        {
            return db.LOGINS.Where(l => l.LOGIN_ID == id && l.USERNAME.Equals(name))
                .SingleOrDefault();
        }

        public LOGIN Get(string name)
        {
            return db.LOGINS.Where(l => l.USERNAME.Equals(name)).FirstOrDefault();
        }



        public LOGIN IsAuthenticated(string token)
        {
            var auth = db.LOGINS.Where(l => l.TOKEN_KEY.Equals(token)).FirstOrDefault();
            if (auth != null && auth.EXPIRE_TIME == null) return auth;
            return null;
        }

        public bool Logout(string authkey,LOGIN exp)
        {
            var auth = IsAuthenticated(authkey);
            if(auth != null)
            {
               return Update(exp);

            }
            return false;
        }

        public bool Update(LOGIN data)
        {
            var u_data = Get(data.LOGIN_ID);
            u_data.EXPIRE_TIME = data.EXPIRE_TIME;
            u_data.USER_LEVEL = data.USER_LEVEL;
            return db.SaveChanges() > 0;
        }
    }
}

using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class AllUsersRepo : IRepo<ALL_USERS, int, string>
    {
        IDEA_XEntities db;
        public AllUsersRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(ALL_USERS data)
        {
            db.ALL_USERS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.ALL_USERS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.ALL_USERS.Remove(data);
            return db.SaveChanges() > 0;
        }

    

        public bool Delete(ALL_USERS data)
        {
            db.ALL_USERS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.ALL_USERS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<ALL_USERS> Get()
        {
            return db.ALL_USERS.ToList();

        }

        public ALL_USERS Get(int id)
        {
            return db.ALL_USERS.Find(id);
        }

        public ALL_USERS Get(int id, string name)
        {
            return db.ALL_USERS.Where(a => a.USERNAME.Equals(name)).SingleOrDefault();
        }

        public ALL_USERS Get(string name)
        {
            return db.ALL_USERS.Where(a => (a.USERNAME.Equals(name,
                StringComparison.OrdinalIgnoreCase) || a.EMAIL.Equals(name)))
                .SingleOrDefault();
        }


        public bool Update(ALL_USERS data)
        {
            var o_d = Get(data.USERNAME);
            if (o_d.PASSWORD.Equals(data.PASSWORD) && o_d.LEVEL.Equals(data.LEVEL)) return true;
            o_d.PASSWORD = data.PASSWORD;
            o_d.LEVEL = data.LEVEL;
            return db.SaveChanges() > 0;
        }
    }
}

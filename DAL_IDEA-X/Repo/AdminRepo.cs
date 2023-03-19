using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class AdminRepo : IRepo<ADMIN, int, string>
    {
        IDEA_XEntities db;
        public AdminRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(ADMIN data)
        {
            db.ADMINS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.ADMINS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.ADMINS.Remove(data);
            return db.SaveChanges() > 0;
        }

       

        public bool Delete(ADMIN data)
        {
            db.ADMINS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.ADMINS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<ADMIN> Get()
        {
            return db.ADMINS.ToList();
        }

        public ADMIN Get(int id)
        {
            return db.ADMINS.Find(id);
        }

        public ADMIN Get(int id, string name)
        {
            return db.ADMINS.Where(a => a.USERNAME.Equals(name)).SingleOrDefault();
        }

        public ADMIN Get(string name)
        {
            return db.ADMINS.FirstOrDefault(u => u.USERNAME.Equals(name,
                StringComparison.OrdinalIgnoreCase));
        }

       

       

        public bool Update(ADMIN data)
        {
            var o_d = Get(data.USERNAME);
            db.Entry(o_d).CurrentValues.SetValues(data);
            return db.SaveChanges() > 0;
        }
    }
}

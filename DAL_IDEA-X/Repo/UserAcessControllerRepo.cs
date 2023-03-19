using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class UserAcessControllerRepo : IRepo<USER_ACCESS_CONTROLLER, int, string>
    {
        IDEA_XEntities db;
        public UserAcessControllerRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(USER_ACCESS_CONTROLLER data)
        {
            db.USER_ACCESS_CONTROLLER.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.USER_ACCESS_CONTROLLER.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.USER_ACCESS_CONTROLLER.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.USER_ACCESS_CONTROLLER.Remove(post);
            return db.SaveChanges() > 0;
        }

        public bool Delete(USER_ACCESS_CONTROLLER data)
        {
            db.USER_ACCESS_CONTROLLER.Remove(data);
            return db.SaveChanges() > 0;
        }

        public List<USER_ACCESS_CONTROLLER> Get()
        {
            return db.USER_ACCESS_CONTROLLER.ToList();
        }

        public USER_ACCESS_CONTROLLER Get(int id)
        {
            return db.USER_ACCESS_CONTROLLER.Find(id);
        }

        public USER_ACCESS_CONTROLLER Get(int id, string name)
        {
            return db.USER_ACCESS_CONTROLLER.Where(u => u.USERNAME.Equals(name))
                .SingleOrDefault();
        }

        public USER_ACCESS_CONTROLLER Get(string name)
        {
            return db.USER_ACCESS_CONTROLLER
                .FirstOrDefault(u => u.USERNAME.Equals(name,
              StringComparison.OrdinalIgnoreCase));
        }

        public bool Update(USER_ACCESS_CONTROLLER data)
        {
            var o_d = Get(data.USERNAME);
            db.Entry(o_d).CurrentValues.SetValues(data);
            return db.SaveChanges() > 0;
        }
    }
}

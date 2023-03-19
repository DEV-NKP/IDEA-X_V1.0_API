using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class UserMessageRepo : IRepo<USER_MESSAGES, int, string>
    {
        IDEA_XEntities db;
        public UserMessageRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(USER_MESSAGES data)
        {
            db.USER_MESSAGES.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.USER_MESSAGES.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.USER_MESSAGES.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.USER_MESSAGES.Remove(post);
            return db.SaveChanges() > 0;
        }

        public bool Delete(USER_MESSAGES data)
        {
            var d = Get(data.MESSAGE_ID);
            db.USER_MESSAGES.Remove(d);
            return db.SaveChanges() > 0;
        }

        public List<USER_MESSAGES> Get()
        {
            return db.USER_MESSAGES.ToList();
        }

        public USER_MESSAGES Get(int id)
        {
            return db.USER_MESSAGES.Where(u => u.MESSAGE_ID == id)
                .SingleOrDefault();

        }

        public USER_MESSAGES Get(int id, string name)
        {
            return db.USER_MESSAGES.Where(m => m.MESSAGE_ID == id && m.SESSION_NAME.Equals(name))
                .SingleOrDefault();
        }

        public USER_MESSAGES Get(string name)
        {
            return db.USER_MESSAGES.Where(m => m.SESSION_NAME.Equals(name))
                .SingleOrDefault();
        }

        public bool Update(USER_MESSAGES data)
        {
            var c_data = Get(data.MESSAGE_ID);
            c_data.USER_MESSAGE = data.USER_MESSAGE;
            return db.SaveChanges() > 0;
        }
    }
}

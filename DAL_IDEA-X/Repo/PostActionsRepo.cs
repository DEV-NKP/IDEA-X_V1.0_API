using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class PostActionsRepo : IRepo<POST_ACTIONS, int, string>
    {
        IDEA_XEntities db;
        public PostActionsRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(POST_ACTIONS data)
        {
            db.POST_ACTIONS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.POST_ACTIONS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.POST_ACTIONS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.POST_ACTIONS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public bool Delete(POST_ACTIONS data)
        {
            db.POST_ACTIONS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public List<POST_ACTIONS> Get()
        {
            return db.POST_ACTIONS.ToList();
        }

        public POST_ACTIONS Get(int id)
        {
            return db.POST_ACTIONS.Where(p => p.POST_ACTION_ID == id)
                .SingleOrDefault();
        }

        public POST_ACTIONS Get(int id, string name)
        {
            return db.POST_ACTIONS.Where(p => p.POST_ID == id &&
           p.USERNAME.Equals(name))
               .SingleOrDefault();
        }

        public POST_ACTIONS Get(string name)
        {
            return db.POST_ACTIONS.Where(p =>
           p.USERNAME.Equals(name))
               .SingleOrDefault();
        }


        public bool Update(POST_ACTIONS data)
        {
            var u = Get((int)data.POST_ACTION_ID);
            u.POST_STATUS = data.POST_STATUS;
            return db.SaveChanges() > 0;
        }
    }
}

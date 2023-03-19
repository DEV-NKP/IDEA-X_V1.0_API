using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class GeneralPostRepo : IRepo<GENERAL_POSTS, int, string>
    {
        IDEA_XEntities db;
        public GeneralPostRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(GENERAL_POSTS data)
        {
            db.GENERAL_POSTS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.GENERAL_POSTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.GENERAL_POSTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        

        public bool Delete(GENERAL_POSTS data)
        {
            db.GENERAL_POSTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.GENERAL_POSTS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<GENERAL_POSTS> Get()
        {
            return db.GENERAL_POSTS.ToList();
        }

        public GENERAL_POSTS Get(int id)
        {
            return db.GENERAL_POSTS.Where(p => p.POST_ID == id).SingleOrDefault();

        }

        public GENERAL_POSTS Get(int id, string name)
        {
            return db.GENERAL_POSTS.Where(p => p.POST_ID == id && 
            p.AUTHOR.Equals(name))
                .SingleOrDefault();
        }

        public GENERAL_POSTS Get(string name)
        {
            return db.GENERAL_POSTS.Where(g => g.AUTHOR.Equals(name)).SingleOrDefault();
        }

        public bool Update(GENERAL_POSTS data)
        {
            var u = Get(data.POST_ID);
            u.TIMELINE_TEXT = data.TIMELINE_TEXT;
            u.TIMELINE_IMAGE = data.TIMELINE_IMAGE;
            u.POST_LIKE = data.POST_LIKE;
            u.POST_DISLIKE = data.POST_DISLIKE;
            u.POST_TAG = data.POST_TAG;
            u.POSTING_STATUS = data.POSTING_STATUS;
            return db.SaveChanges() > 0;
        }
    }
}

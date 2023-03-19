using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class MessageRequestRepo : IRepo<MESSAGE_REQUESTS, int, string>
    {
        IDEA_XEntities db;
        public MessageRequestRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(MESSAGE_REQUESTS data)
        {
            db.MESSAGE_REQUESTS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.MESSAGE_REQUESTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.MESSAGE_REQUESTS.Remove(data);
            return db.SaveChanges() > 0;
        }

     

        public bool Delete(MESSAGE_REQUESTS data)
        {
            var res = Get(data.REQUEST_ID);
            if(res != null)
            {
                db.MESSAGE_REQUESTS.Remove(res);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.MESSAGE_REQUESTS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<MESSAGE_REQUESTS> Get()
        {
            return db.MESSAGE_REQUESTS.ToList();
        }

        public MESSAGE_REQUESTS Get(int id)
        {
            var data = db.MESSAGE_REQUESTS.Where(m => m.REQUEST_ID == id).FirstOrDefault();
            return data;
        }

        public MESSAGE_REQUESTS Get(int id, string name)
        {
            return db.MESSAGE_REQUESTS.Where(m => m.REQUEST_ID == id && m.SENDER.Equals(name))
                .SingleOrDefault();
        }

        public MESSAGE_REQUESTS Get(string name)
        {
            return db.MESSAGE_REQUESTS.Where(l => l.SENDER.Equals(name)).FirstOrDefault();
        }




        public bool Update(MESSAGE_REQUESTS data)
        {
            var d = Get(data.REQUEST_ID);
            d.MESSAGE_TIME = data.MESSAGE_TIME;
            return db.SaveChanges() > 0;
        }
    }
}

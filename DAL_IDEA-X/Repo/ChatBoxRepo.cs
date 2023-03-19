using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class ChatBoxRepo : IRepo<CHAT_BOXS, int, string>
    {
        IDEA_XEntities db;
        public ChatBoxRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(CHAT_BOXS data)
        {
            db.CHAT_BOXS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.CHAT_BOXS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.CHAT_BOXS.Remove(data);
            return db.SaveChanges() > 0;
        }

       

        public bool Delete(CHAT_BOXS data)
        {
            var d = Get(data.CHAT_SESSION);
            db.CHAT_BOXS.Remove(d);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.CHAT_BOXS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<CHAT_BOXS> Get()
        {
            return db.CHAT_BOXS.ToList();
        }

        public CHAT_BOXS Get(int id)
        {
            return db.CHAT_BOXS.Find(id);
        }

        public CHAT_BOXS Get(int id, string name)
        {
            return db.CHAT_BOXS.Where(c => c.CHAT_SESSION.Equals(name)).SingleOrDefault();
        }

        public CHAT_BOXS Get(string name)
        {
            return db.CHAT_BOXS.Where(cb => cb.CHAT_SESSION.Equals(name))
                .SingleOrDefault();
        }


        public bool Update(CHAT_BOXS data)
        {
            var c_data = Get(data.CHAT_SESSION);
            db.CHAT_BOXS.Remove(c_data);
            CHAT_BOXS cHAT_BOXS = new CHAT_BOXS()
            {
                CHAT_SESSION = $"{data.SENDER}_TO_{data.RECEIVER}",
                CHAT_TIME = data.CHAT_TIME,
                RECEIVER = data.RECEIVER,
                SENDER = data.SENDER
            };
            db.CHAT_BOXS.Add(cHAT_BOXS);
            
            return db.SaveChanges() > 0;
        }
    }
}

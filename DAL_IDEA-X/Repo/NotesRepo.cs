using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class NotesRepo : IRepo<NOTE, int, string>
    {
        IDEA_XEntities db;
        public NotesRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(NOTE data)
        {
            db.NOTES.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.NOTES.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.NOTES.Remove(data);
            return db.SaveChanges() > 0;
        }

       

        public bool Delete(NOTE data)
        {
            db.NOTES.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.NOTES.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<NOTE> Get()
        {
            return db.NOTES.ToList();
        }

        public NOTE Get(int id)
        {
            return db.NOTES.Where(n => n.NOTE_ID == id).SingleOrDefault();
        }

        public NOTE Get(int id, string name)
        {
            return db.NOTES.Where(n => n.NOTE_ID == id && n.NOTE_TEXT.Equals(name))
                .SingleOrDefault();
        }

        public NOTE Get(string name)
        {
            return db.NOTES.Where(n => n.NOTE_TEXT.Equals(name))
                .SingleOrDefault();
        }
        public bool Update(NOTE data)
        {
            
            var o_d = Get(data.NOTE_ID);
            o_d.NOTE_TEXT = data.NOTE_TEXT;
            return db.SaveChanges() > 0;
        }
    }
}

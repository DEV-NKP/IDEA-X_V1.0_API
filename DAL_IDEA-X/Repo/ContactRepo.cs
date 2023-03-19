using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class ContactRepo : IRepo<CONTACT, int, string>
    {
        IDEA_XEntities db;
        public ContactRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(CONTACT data)
        {
            db.CONTACTS.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.CONTACTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.CONTACTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(CONTACT data)
        {
            db.CONTACTS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.CONTACTS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public List<CONTACT> Get()
        {
            return db.CONTACTS.ToList();
        }

        public CONTACT Get(int id)
        {
            var data = db.CONTACTS.Find(id);
            return data;
        }

        public CONTACT Get(int id, string name)
        {
            return db.CONTACTS.Where(c => c.CONTACT_ID == id && c.USERNAME.Equals(name))
                .SingleOrDefault();
        }

        public CONTACT Get(string name)
        {
            return db.CONTACTS.Where(c => c.USERNAME.Equals(name)).SingleOrDefault();
        }



        public bool Update(CONTACT data)
        {
            var u = Get(data.CONTACT_ID);
            if(data.MESSAGE != null) u.MESSAGE = data.MESSAGE; 
            u.STATUS = data.STATUS;
            return db.SaveChanges() > 0;
        }

    }
}

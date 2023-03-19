using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class UserDetailsRepo : IRepo<USER_DETAILS, int, string>
    {
        IDEA_XEntities db;
        public UserDetailsRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(USER_DETAILS data)
        {
            db.USER_DETAILS.Add(data);
            return db.SaveChanges() > 0;

        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.USER_DETAILS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.USER_DETAILS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.USER_DETAILS.Remove(post);
            return db.SaveChanges() > 0;
        }

        public bool Delete(USER_DETAILS data)
        {
            db.USER_DETAILS.Remove(data);
            return db.SaveChanges() > 0;
        }

        public List<USER_DETAILS> Get()
        {
            return db.USER_DETAILS.ToList();
        }

        public USER_DETAILS Get(int id)
        {
            return db.USER_DETAILS.Find(id);
        }

        public USER_DETAILS Get(int id, string name)
        {
            return db.USER_DETAILS.Where(u => u.USERNAME.Equals(name)).SingleOrDefault();
        }

        public USER_DETAILS Get(string name)
        {
            return db.USER_DETAILS.FirstOrDefault(u => u.USERNAME.Equals(name,
                StringComparison.OrdinalIgnoreCase));
        }


        public bool Update(USER_DETAILS data)
        {
            var o_d = Get(data.USERNAME);
            o_d.COUNTRY = data.COUNTRY;
            o_d.DATE_OF_BIRTH = data.DATE_OF_BIRTH;
            o_d.FIRST_NAME = data.FIRST_NAME;
            o_d.GENDER = data.GENDER;
            o_d.USER_ADDRESS = data.USER_ADDRESS;
            o_d.LAST_NAME = data.LAST_NAME;
            o_d.MOBILE = data.MOBILE;
            o_d.EDUCATIONAL_INSTITUTION = data.EDUCATIONAL_INSTITUTION;
            o_d.DEPARTMENT = data.DEPARTMENT;
            o_d.HEADLINE = data.HEADLINE;
            return db.SaveChanges() > 0;
        }
    }
}

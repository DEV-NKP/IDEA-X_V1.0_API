using DAL_IDEA_X.EntityFramework;
using DAL_IDEA_X.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Repo
{
    internal class PostReportRepo : IRepo<POST_REPORT, int, string>
    {
        IDEA_XEntities db;
        public PostReportRepo(IDEA_XEntities db)
        {
            this.db = db;
        }
        public bool Create(POST_REPORT data)
        {
            db.POST_REPORT.Add(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            db.POST_REPORT.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string name)
        {
            var data = Get(name);
            db.POST_REPORT.Remove(data);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id, string name)
        {
            var post = Get(id, name);
            db.POST_REPORT.Remove(post);
            return db.SaveChanges() > 0;
        }

        public bool Delete(POST_REPORT data)
        {
            db.POST_REPORT.Remove(data);
            return db.SaveChanges() > 0;
        }

        public List<POST_REPORT> Get()
        {
            return db.POST_REPORT.ToList();
        }

        public POST_REPORT Get(int id)
        {
            return db.POST_REPORT.Where(p => p.POST_ID == id)
                .SingleOrDefault();
        }

        public POST_REPORT Get(int id, string name)
        {
            return db.POST_REPORT.Where(p => p.POST_ID == id && p.REPORTED_BY.Equals(name))
                .SingleOrDefault();
        }

        public POST_REPORT Get(string name)
        {
            return db.POST_REPORT.Where(p => p.REPORTED_BY.Equals(name))
                .SingleOrDefault();
        }


        

        public bool Update(POST_REPORT data)
        {
            var u = Get((int)data.POST_ID);
            u.REPORT_STATUS = data.REPORT_STATUS;
            return db.SaveChanges() > 0;
        }
    }
}

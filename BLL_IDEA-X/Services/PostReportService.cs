using BLL_IDEA_X.Helper_Classes;
using DAL_IDEA_X;
using DAL_IDEA_X.EntityFramework;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class PostReportService
    {
        //Read -- kawshik
        public static List<PostReportModel> GetPostReportListByID(int id)
        {
            var data = DataAcessFactory.PostRepoDataAccess().Get();
            var list = data.Where(r => r.POST_ID == id).ToList();
            List<PostReportModel> postReports = new List<PostReportModel>();
            foreach (var item in data)
            {
                postReports.Add(new PostReportModel()
                {
                    POST_AUTHOR = item.POST_AUTHOR,
                    POST_ID = (int)item.POST_ID,
                    REPORTED_BY = item.REPORTED_BY,
                    REPORT_CATEGORY = item.REPORT_CATEGORY,
                    REPORT_DETAILS = item.REPORT_DETAILS,
                    REPORT_IP = item.REPORT_IP,
                    REPORT_STATUS = item.REPORT_STATUS,
                    REPORT_TIME = item.REPORT_TIME,
                });

            }
            return postReports;
        }
        public static List<PostReportModel> GetPostReportListByReporter(string name)
        {
            var data = DataAcessFactory.PostRepoDataAccess().Get();
            var list = data.Where(r => r.REPORTED_BY.Equals(name)).ToList();
            List<PostReportModel> postReports = new List<PostReportModel>();
            foreach (var item in list)
            {
                postReports.Add(new PostReportModel()
                {
                    POST_AUTHOR = item.POST_AUTHOR,
                    POST_ID = (int)item.POST_ID,
                    REPORTED_BY = item.REPORTED_BY,
                    REPORT_CATEGORY = item.REPORT_CATEGORY,
                    REPORT_DETAILS = item.REPORT_DETAILS,
                    REPORT_IP = item.REPORT_IP,
                    REPORT_STATUS = item.REPORT_STATUS,
                    REPORT_TIME = item.REPORT_TIME,
                });

            }
            return postReports;
        }


        public static List<PostReportModelWithID> GetAllPostReport()
        {
            var data = DataAcessFactory.PostRepoDataAccess().Get();
            if (data != null)
            {
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in data)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }
        public static List<PostReportModelWithID> PendingReport()
        {
            var data = GetAllPostReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_STATUS.Equals("PENDING")
                               orderby u_d.REPORT_ID ascending
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }

        public static List<PostReportModelWithID> InvestigatingReport()
        {
            var data = GetAllPostReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_STATUS.Equals("INVESTIGATING")
                               orderby u_d.REPORT_ID ascending
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }
        public static List<PostReportModelWithID> ClosedReport()
        {
            var data = GetAllPostReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_STATUS.Equals("CLOSED")
                               orderby u_d.REPORT_ID ascending
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }
        public static List<PostReportModelWithID> SearchPendingReport(String text)
        {
            var data = PendingReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_ID.ToString().Contains(text) ||
                             u_d.POST_ID.ToString().Contains(text) ||
                             u_d.POST_AUTHOR.Contains(text) ||
                             u_d.REPORTED_BY.Contains(text) ||
                              u_d.REPORT_CATEGORY.Contains(text) ||
                             u_d.REPORT_TIME.Contains(text) ||
                             u_d.REPORT_IP.Contains(text) ||
                             u_d.REPORT_STATUS.Contains(text)
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }
        public static List<PostReportModelWithID> SearchInvestigatingReport(String text)
        {
            var data = InvestigatingReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_ID.ToString().Contains(text) ||
                             u_d.POST_ID.ToString().Contains(text) ||
                             u_d.POST_AUTHOR.Contains(text) ||
                             u_d.REPORTED_BY.Contains(text) ||
                              u_d.REPORT_CATEGORY.Contains(text) ||
                             u_d.REPORT_TIME.Contains(text) ||
                             u_d.REPORT_IP.Contains(text) ||
                             u_d.REPORT_STATUS.Contains(text)
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }

        public static List<PostReportModelWithID> SearchClosedReport(String text)
        {
            var data = ClosedReport();

            if (data != null)
            {
                var reports = (from u_d in data
                               where u_d.REPORT_ID.ToString().Contains(text) ||
                             u_d.POST_ID.ToString().Contains(text) ||
                             u_d.POST_AUTHOR.Contains(text) ||
                             u_d.REPORTED_BY.Contains(text) ||
                              u_d.REPORT_CATEGORY.Contains(text) ||
                             u_d.REPORT_TIME.Contains(text) ||
                             u_d.REPORT_IP.Contains(text) ||
                             u_d.REPORT_STATUS.Contains(text)
                               select u_d).ToList();
                List<PostReportModelWithID> postReports = new List<PostReportModelWithID>();
                foreach (var item in reports)
                {
                    postReports.Add(new PostReportModelWithID()
                    {
                        POST_AUTHOR = item.POST_AUTHOR,
                        POST_ID = (int)item.POST_ID,
                        REPORTED_BY = item.REPORTED_BY,
                        REPORT_CATEGORY = item.REPORT_CATEGORY,
                        REPORT_DETAILS = item.REPORT_DETAILS,
                        REPORT_IP = item.REPORT_IP,
                        REPORT_STATUS = item.REPORT_STATUS,
                        REPORT_TIME = item.REPORT_TIME,
                        REPORT_ID = item.REPORT_ID
                    });

                }
                return postReports;
            }
            return null;
        }

        //Read -- kawshik
        public static PostReportModelWithID GetPostReportwithIDByID(int id)
        {
            var data = DataAcessFactory.PostRepoDataAccess().Get(id);
            if (data != null)
            {
                PostReportModelWithID postReport = new PostReportModelWithID()
                {
                    POST_AUTHOR = data.POST_AUTHOR,
                    POST_ID = (int)data.POST_ID,
                    REPORTED_BY = data.REPORTED_BY,
                    REPORT_CATEGORY = data.REPORT_CATEGORY,
                    REPORT_DETAILS = data.REPORT_DETAILS,
                    REPORT_IP = data.REPORT_IP,
                    REPORT_STATUS = data.REPORT_STATUS,
                    REPORT_TIME = data.REPORT_TIME,
                    REPORT_ID = data.REPORT_ID,
                };
                return postReport;
            }
            return null;

        }
        public static PostReportModelWithID GetPostReportwithReportID(int rid)
        {
            var data = GetAllPostReport();
            
            if (data != null)
            {
                var rep = (from pa in data.Where(x => x.REPORT_ID == rid)
                           select pa).SingleOrDefault();
                if(rep != null)
                {
                    PostReportModelWithID postReport = new PostReportModelWithID()
                    {
                        POST_AUTHOR = rep.POST_AUTHOR,
                        POST_ID = rep.POST_ID,
                        REPORTED_BY = rep.REPORTED_BY,
                        REPORT_CATEGORY = rep.REPORT_CATEGORY,
                        REPORT_DETAILS = rep.REPORT_DETAILS,
                        REPORT_IP = rep.REPORT_IP,
                        REPORT_STATUS = rep.REPORT_STATUS,
                        REPORT_TIME = rep.REPORT_TIME,
                        REPORT_ID = rep.REPORT_ID,
                    };
                    return postReport;
                }
                return null;
            }
            return null;

        }
        public static PostReportModelWithID GetPostReportwithIDByUname(int id, string uname)
        {
            var data = DataAcessFactory.PostRepoDataAccess().Get(id, uname);
            if (data != null)
            {
                PostReportModelWithID postReport = new PostReportModelWithID()
                {
                    POST_AUTHOR = data.POST_AUTHOR,
                    POST_ID = (int)data.POST_ID,
                    REPORTED_BY = data.REPORTED_BY,
                    REPORT_CATEGORY = data.REPORT_CATEGORY,
                    REPORT_DETAILS = data.REPORT_DETAILS,
                    REPORT_IP = data.REPORT_IP,
                    REPORT_STATUS = data.REPORT_STATUS,
                    REPORT_TIME = data.REPORT_TIME,
                    REPORT_ID = data.REPORT_ID,
                };
                return postReport;
            }
            return null;

        }
        //Delete -- kawshik
        public static bool RemovePostReport(int id)
        {
            if (id != 0)
            {
                var post = GetPostReportwithIDByID(id);
                if (post != null)
                {
                    return DataAcessFactory.PostRepoDataAccess().Delete(id);
                }
                return false;
            }
            return false;
        }

        public static bool RevokePostReport(int id)
        {
            if (id != 0)
            {
                var post = GetPostReportwithReportID(id);
                if (post != null && post.REPORT_STATUS.Equals("PENDING"))
                {
                    return DataAcessFactory.PostRepoDataAccess().Delete(id);
                }
                return false;
            }
            return false;
        }

        //Create -- kawshik
        public static bool ReportPost(PostReportModel repreq)
        {
            if (repreq != null)
            {
                if (repreq.REPORT_DETAILS != null || repreq.REPORT_CATEGORY != null)
                {
                    repreq.REPORT_TIME = IDEAX_Functions.time();
                    repreq.REPORT_IP = IDEAX_Functions.ip();
                    repreq.REPORT_STATUS = "PENDING";
                    var post = GeneralPostService.GetPostByID(repreq.POST_ID);

                    POST_REPORT newreport = new POST_REPORT();

                    newreport.POST_ID = post.POST_ID;
                    newreport.POST_AUTHOR = post.AUTHOR;
                    newreport.REPORT_CATEGORY = repreq.REPORT_CATEGORY;
                    newreport.REPORT_DETAILS = repreq.REPORT_DETAILS;
                    newreport.REPORT_TIME = repreq.REPORT_TIME;
                    newreport.REPORT_IP = repreq.REPORT_IP;
                    newreport.REPORT_STATUS = repreq.REPORT_STATUS;
                    newreport.REPORTED_BY = repreq.REPORTED_BY;

                    return DataAcessFactory.PostRepoDataAccess().Create(newreport);
                }
                return false;
            }
            return false;
        }
        //Update -- kawshik
        public static bool UpdateReportStatus(int id,string status)
        {
           
                var data = GetPostReportwithReportID(id);
                if (data != null)
                {
                    data.REPORT_STATUS = status;
                    POST_REPORT pr = new POST_REPORT()
                    {
                        POST_AUTHOR = data.POST_AUTHOR,
                        POST_ID = (int)data.POST_ID,
                        REPORTED_BY = data.REPORTED_BY,
                        REPORT_CATEGORY = data.REPORT_CATEGORY,
                        REPORT_DETAILS = data.REPORT_DETAILS,
                        REPORT_IP = data.REPORT_IP,
                        REPORT_STATUS = data.REPORT_STATUS,
                        REPORT_TIME = data.REPORT_TIME,
                        REPORT_ID = data.REPORT_ID,

                    };
                    return DataAcessFactory.PostRepoDataAccess().Update(pr);
                }
                return false;
           
        }

    }
}

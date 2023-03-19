using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_IDEA_X.Services
{
    public class ExcelInfoDownloadService
    {

        public static string DownloadUserLoginInfo(string uname)
        {
            var users = LoginService.GetUserLoginInfoList(uname);

            if(users.Count > 0)
            {
                DataTable dt = new DataTable("Grid");



                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("UserName"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("LoginTime"),
                                                     new DataColumn("LoginIP"),
                                                 });
                foreach (var item in users)
                {

                    dt.Rows.Add(item.USERNAME, item.EMAIL, item.LOGIN_TIME, item.LOGIN_IP);

                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }


        public static string DownloadUserPostInfo(string uname)
        {

            var posts = GeneralPostService.GetActivePostWithActionsAndTime(uname);

            if(posts.Count > 0)
            {
                DataTable dt = new DataTable("Grid");



                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Author"),
                                                     new DataColumn("Timeline_Text"),
                                                     new DataColumn("Timeline_Image"),
                                                     new DataColumn("Posting_Time"),
                                                     new DataColumn("Post_Tag"),
                                                     new DataColumn("Post_Status"),
                                                     new DataColumn("Total_Upvote"),
                                                     new DataColumn("Total_Downvote"),
                                                     new DataColumn("Voted_By"),
                                                     new DataColumn("Vote"),

                                                 });
                foreach (var item in posts)
                {
                    var img = "";
                    if (item.TIMELINE_IMAGE != null)
                    {
                        img += "This post contain an image in IDEA-X";
                    }
                    else
                    {
                        img += "No image for this post in IDEA-X";
                    }
                    dt.Rows.Add(item.AUTHOR, item.TIMELINE_TEXT, img, item.POSTING_TIME, item.POST_TAG, 
                        item.POSTING_STATUS, 
                        item.POST_LIKE, item.POST_DISLIKE, item.AUTHOR, item.POST_STATUS);

                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadUserReportInfo(string uname)
        {

            var posts = PostReportService.GetPostReportListByReporter(uname);
            if(posts.Count > 0)
            {
                DataTable dt = new DataTable("Grid");



                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Post_ID"),
                                                     new DataColumn("Post_Author"),
                                                     new DataColumn("Report_Category"),
                                                     new DataColumn("Report_Details"),
                                                     new DataColumn("Report_Time"),
                                                     new DataColumn("Report_IP"),
                                                     new DataColumn("Report_Status"),

                                                 });
                foreach (var item in posts)
                {

                    dt.Rows.Add(item.POST_ID, item.POST_AUTHOR, item.REPORT_CATEGORY, item.REPORT_DETAILS, item.REPORT_TIME, item.REPORT_IP, item.REPORT_STATUS);

                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadUserLogIn_Admin()
        {

            var dulog = LoginService.GetUserLoginInfo();
            if (dulog.Count > 0)
            {
                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Login_ID"),
                                                     new DataColumn("Username"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Login_Time"),
                                                     new DataColumn("Login_IP"),

                                                 });
                foreach (var item in dulog)
                {
                    dt.Rows.Add(item.LOGIN_ID, item.USERNAME, item.EMAIL, item.LOGIN_TIME, item.LOGIN_IP);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }
        public static string DownloadUserList_Admin()
        {

            var ul = AllUserService.GetAllUser();
            if (ul.Count > 0)
            {
                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Username"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Level"),
                                                 });
                foreach (var item in ul)
                {
                    dt.Rows.Add(item.USERNAME, item.EMAIL, item.LEVEL);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadAllUserDetails_Admin()
        {

            var dalluser = UserDetailsService.GetUserDetails();
            if (dalluser.Count > 0)
            {
                DataTable dt = new DataTable("Grid");



                dt.Columns.AddRange(new DataColumn[19] { new DataColumn("Username"),
                                                     new DataColumn("First_Name"),
                                                     new DataColumn("Last_Name"),
                                                     new DataColumn("Headline"),
                                                     new DataColumn("Date_Of_Birth"),
                                                     new DataColumn("Gender"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("User_Address"),
                                                     new DataColumn("User_State"),
                                                     new DataColumn("Zip_Code"),
                                                     new DataColumn("Country"),
                                                     new DataColumn("Industry"),
                                                     new DataColumn("Educational_Institution"),
                                                     new DataColumn("Department"),
                                                     new DataColumn("Contact_URL"),
                                                     new DataColumn("Profile_Picture"),
                                                     new DataColumn("SignUp_Time"),
                                                     new DataColumn("User_Status"),
                                                     new DataColumn("SignUp_IP"),

                                                 });
                foreach (var item in dalluser)
                {
                    var img = "";
                    if (item.PROFILE_PICTURE != null)
                    {
                        img += "This post contain an image in IDEA-X";
                    }
                    else
                    {
                        img += "No image for this post in IDEA-X";
                    }
                    dt.Rows.Add(item.USERNAME, item.FIRST_NAME, item.LAST_NAME, item.HEADLINE, item.DATE_OF_BIRTH, item.GENDER, item.MOBILE, item.USER_ADDRESS, item.USER_STATE, item.ZIP_CODE, item.COUNTRY, item.INDUSTRY, item.EDUCATIONAL_INSTITUTION, item.DEPARTMENT, item.CONTACT_URL, img, item.SIGNUP_TIME, item.USER_STATUS, item.SIGNUP_IP);

                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadMessageRequests_Admin()
        {

            var req = MessageRequestService.GetAllReqList();
            if (req.Count > 0)
            {
                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Request_ID"),
                                                     new DataColumn("Sender"),
                                                     new DataColumn("Receiver"),
                                                     new DataColumn("Message_Time"),

                                                 });
                foreach (var item in req)
                {
                    dt.Rows.Add(item.REQUEST_ID, item.SENDER, item.RECEIVER, item.MESSAGE_TIME);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadChatSession_Admin()
        {

            var cs = ChatBoxService.ChatSessions();
            if (cs.Count > 0)
            {
                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Chat_Session"),
                                                     new DataColumn("Sender"),
                                                     new DataColumn("Receiver"),
                                                     new DataColumn("Chat_Time"),

                                                 });
                foreach (var item in cs)
                {
                    dt.Rows.Add(item.CHAT_SESSION, item.SENDER, item.RECEIVER, item.CHAT_TIME);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadPostArchives_Admin()
        {

            var dparch = GeneralPostService.GetPostWithActions();
            if (dparch.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Post_ID"),
                                                     new DataColumn("Author"),
                                                     new DataColumn("Timeline_Text"),
                                                     new DataColumn("Timeline_Image"),
                                                     new DataColumn("Posting_Time"),
                                                     new DataColumn("Posting_Status"),
                                                     new DataColumn("Post_Like"),
                                                     new DataColumn("Post_Dislike"),
                                                     new DataColumn("Post_IP"),
                                                     new DataColumn("Post_Tag"),

                                                 });
                foreach (var item in dparch)
                {
                    var img = "";
                    if (item.TIMELINE_IMAGE != null)
                    {
                        img += "This post contain an image in IDEA-X";
                    }
                    else
                    {
                        img += "No image for this post in IDEA-X";
                    }
                    dt.Rows.Add(item.POST_ID, item.AUTHOR, item.TIMELINE_TEXT, img, item.POSTING_TIME, item.POSTING_STATUS, item.POST_LIKE, item.POST_DISLIKE, item.POST_IP, item.POST_TAG);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadPendingReports_Admin()
        {

            var reports = PostReportService.PendingReport();
            if (reports.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Report_ID"),
                                                     new DataColumn("Post_ID"),
                                                     new DataColumn("Post_Author"),
                                                     new DataColumn("Reported_By"),
                                                     new DataColumn("Report_Category"),
                                                     new DataColumn("Report_Details"),
                                                     new DataColumn("Report_Time"),
                                                     new DataColumn("Report_IP"),
                                                     new DataColumn("Report_Status"),

                                                 });
                foreach (var item in reports)
                {
                    dt.Rows.Add(item.REPORT_ID,
                        item.POST_ID,
                        item.POST_AUTHOR,
                        item.REPORTED_BY,
                        item.REPORT_CATEGORY,
                        item.REPORT_DETAILS,
                        item.REPORT_TIME,
                        item.REPORT_IP,
                        item.REPORT_STATUS
                        );
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadInvestigatingReports_Admin()
        {

            var reports = PostReportService.InvestigatingReport();
            if (reports.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Report_ID"),
                                                     new DataColumn("Post_ID"),
                                                     new DataColumn("Post_Author"),
                                                     new DataColumn("Reported_By"),
                                                     new DataColumn("Report_Category"),
                                                     new DataColumn("Report_Details"),
                                                     new DataColumn("Report_Time"),
                                                     new DataColumn("Report_IP"),
                                                     new DataColumn("Report_Status"),

                                                 });
                foreach (var item in reports)
                {
                    dt.Rows.Add(item.REPORT_ID,
                        item.POST_ID,
                        item.POST_AUTHOR,
                        item.REPORTED_BY,
                        item.REPORT_CATEGORY,
                        item.REPORT_DETAILS,
                        item.REPORT_TIME,
                        item.REPORT_IP,
                        item.REPORT_STATUS
                        );
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadUserController_Admin()
        {

            var dulog = UACService.GetUserControllerList();
            if (dulog.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[2] {
                                                     new DataColumn("Username"),
                                                     new DataColumn("Email")


                                                 });
                foreach (var item in dulog)
                {
                    dt.Rows.Add(item.USERNAME, item.EMAIL);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }


        public static string DownloadClosedReports_Admin()
        {

            var reports = PostReportService.ClosedReport();
            if (reports.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Report_ID"),
                                                     new DataColumn("Post_ID"),
                                                     new DataColumn("Post_Author"),
                                                     new DataColumn("Reported_By"),
                                                     new DataColumn("Report_Category"),
                                                     new DataColumn("Report_Details"),
                                                     new DataColumn("Report_Time"),
                                                     new DataColumn("Report_IP"),
                                                     new DataColumn("Report_Status"),

                                                 });
                foreach (var item in reports)
                {
                    dt.Rows.Add(item.REPORT_ID,
                        item.POST_ID,
                        item.POST_AUTHOR,
                        item.REPORTED_BY,
                        item.REPORT_CATEGORY,
                        item.REPORT_DETAILS,
                        item.REPORT_TIME,
                        item.REPORT_IP,
                        item.REPORT_STATUS
                        );
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }


        public static string DownloadPendingContacts_Admin()
        {

            var reports = ContactService.PendingContact();
            if (reports.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("CONTACT_ID"),
                                                     new DataColumn("FIRST_NAME"),
                                                     new DataColumn("LAST_NAME"),
                                                     new DataColumn("USERNAME"),
                                                     new DataColumn("EMAIL"),
                                                     new DataColumn("MESSAGE"),
                                                     new DataColumn("LEVEL"),
                                                     new DataColumn("STATUS"),
                                                     new DataColumn("LOGIN_TIME"),
                                                     new DataColumn("LOGIN_IP")

                                                 });
                foreach (var item in reports)
                {
                    dt.Rows.Add(
                        item.CONTACT_ID,
                        item.FIRST_NAME,
                        item.LAST_NAME,
                        item.USERNAME,
                        item.EMAIL,
                        item.MESSAGE,
                        item.LEVEL,
                        item.STATUS,
                        item.LOGIN_TIME,
                        item.LOGIN_IP
                        );
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }

        public static string DownloadSolvedContacts_Admin()
        {

            var reports = ContactService.SolvedContact();
            if (reports.Count > 0)
            {

                DataTable dt = new DataTable("Grid");

                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("CONTACT_ID"),
                                                     new DataColumn("FIRST_NAME"),
                                                     new DataColumn("LAST_NAME"),
                                                     new DataColumn("USERNAME"),
                                                     new DataColumn("EMAIL"),
                                                     new DataColumn("MESSAGE"),
                                                     new DataColumn("LEVEL"),
                                                     new DataColumn("STATUS"),
                                                     new DataColumn("LOGIN_TIME"),
                                                     new DataColumn("LOGIN_IP")

                                                 });
                foreach (var item in reports)
                {
                    dt.Rows.Add(
                        item.CONTACT_ID,
                        item.FIRST_NAME,
                        item.LAST_NAME,
                        item.USERNAME,
                        item.EMAIL,
                        item.MESSAGE,
                        item.LEVEL,
                        item.STATUS,
                        item.LOGIN_TIME,
                        item.LOGIN_IP
                        );
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);

                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return null;
        }
    }
}

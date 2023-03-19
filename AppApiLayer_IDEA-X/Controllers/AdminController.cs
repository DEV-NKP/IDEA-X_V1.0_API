using AppApiLayer_IDEA_X.Auth;
using BLL_IDEA_X.Services;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AppApiLayer_IDEA_X.Controllers
{

    public class AdminController : ApiController
    {
        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/AddAdminInfo")]
        public HttpResponseMessage AddAdminInfo(AdminModel a)
        {
            if (ModelState.IsValid)
            {
                var output = AdminService.AddAdminInfo(a);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Admin info added sucessfully");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error adding admin info: Missing or not valid json data");
        }
        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/ChangeAdminPass")]

        public HttpResponseMessage ChangeAdminPass(UpdatePassModel passModel)
        {
            if (passModel != null)
            {
                var output = AdminService.ChangeAdminPass(passModel);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Admin Password Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during updating admin password : Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during updating admin password : Missing or not valid json data");
        }
        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/RemoveAdminInfo")]
        public HttpResponseMessage RemoveAdminInfo(AdminModel admin)
        {

            if (admin != null)
            {
                var output = AdminService.RemoveAdminInfo(admin.USERNAME);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Admin info removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing admin info : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing admin info : Missing or not valid json data");

        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/GetAdminInfo/{name}")]
        public HttpResponseMessage GetAdminInfo(string name)
        {
            var data = AdminService.GetAdminInfo(name);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }


        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/GetAllUACInfo")]
        public HttpResponseMessage GetAllUACInfo()
        {
            var data = UACService.GetUserControllerList();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                "Error getting user acess controller info list : application error");
        }
        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/AddUACInfo")]
        public HttpResponseMessage AddUACInfo(UACModel uAC)
        {
            if (uAC != null && ModelState.IsValid)
            {
                var output = UACService.AddUserController(uAC);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "User controller info added sucessfully");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error adding user controller info: Missing or not valid json data");
        }

        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/RemoveUACInfo")]
        public HttpResponseMessage RemoveUACInfo(UACModel uAC)
        {

            if (uAC != null)
            {
                var output = UACService.RemoveUserController(uAC.USERNAME);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "User Controller info removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing user controller info : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing user controller info : Missing or not valid json data");

        }

        [HttpGet]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/GetPostWithProfileImage/{uname}")]
        public HttpResponseMessage GetPostWithProfileImage(string uname)
        {
            var data = GeneralPostService.GetPostWithProfileImageAdmin(uname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                "Error getting post list : Application error");
        }

        [HttpPost]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/BanPost")]
        public HttpResponseMessage BanPost(GeneralPostEditModel editModel)
        {

            if (editModel != null && editModel.POST_ID != 0)
            {
                var output = GeneralPostService.ChangePostStatus(editModel.POST_ID, "BANNED");
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Post banned Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during banning post : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during banning post : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/UnBanPost")]
        public HttpResponseMessage UnBanPost(GeneralPostEditModel editModel)
        {

            if (editModel != null && editModel.POST_ID != 0)
            {
                var output = GeneralPostService.ChangePostStatus(editModel.POST_ID, "ACTIVE");
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Post unbanned Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during unbanning post : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during unbanning post : Missing or not valid json data");

        }

        [HttpGet]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/SearchPost/{text}")]
        public HttpResponseMessage SearchPost(string text)
        {
            if (text != null && text.Length > 0)
            {
                var output = GeneralPostService.SearchPostAdmin(text);
                if (output.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/SearchPost/{uname}/{id}")]
        public HttpResponseMessage SearchPost(string uname, int id)
        {
            if (uname != null && id != 0)
            {
                var output = GeneralPostService.SearchPostViewAdmin(uname, id);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/Profile")]
        public HttpResponseMessage Profile()
        {

            var output = AdminService.GetAdminProfile();
            if (output.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting search post data: Invalid request");

        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/GetUserActivity/{uname}")]
        public HttpResponseMessage GetUserActivity(string uname)
        {


            var output = UserActivityService.GetUserActivitiesAdmin(uname);
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting user activity data: Invalid request");


        }


        [HttpGet]
        [Auth("ADMIN", "UAC")]
        [Route("api/admin/SearchUserActivityProgress/{uname}")]
        public HttpResponseMessage SearchUserActivityProgress(string uname)
        {
            if (uname != null && uname.Length > 0)
            {
                var output = UserActivityService.SearchUserActivityProgress(uname);
                if (output.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user activity: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search  user activity: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/UserSearch/{uname}")]
        public HttpResponseMessage UserSearch(string uname)
        {
            if (uname != null)
            {
                var output = UserDetailsService.UserSearch(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/SearchProfile/{text}")]
        public HttpResponseMessage SearchProfile(string text)
        {
            if (text != null)
            {
                var output = UserDetailsService.SearchProfile(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Missing or not valid json data");
        }

        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/BanUser")]
        public HttpResponseMessage BanUser(AllUserModel editModel)
        {

            if (editModel != null && editModel.USERNAME != null)
            {
                var output = AllUserService.UpdateUserLevel(editModel.USERNAME, "BANNED");
                LoginService.UpdateUserLoginLevel(editModel.USERNAME, "BANNED");
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "User banned Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during banning user : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during banning user : Missing or not valid json data");

        }

        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/UnBanUser")]
        public HttpResponseMessage UnBanUser(AllUserModel editModel)
        {

            if (editModel != null && editModel.USERNAME != null)
            {
                var output = AllUserService.UpdateUserLevel(editModel.USERNAME, "USER");
                LoginService.UpdateUserLoginLevel(editModel.USERNAME, "USER");
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "User unbanned Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during unbanning user : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during unbanning user : Missing or not valid json data");

        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/Monitoring")]
        public HttpResponseMessage Monitoring()
        {
            var output = UserActivityService.Monitoring();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting monitoring data: Server Error");

        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchActivityProgress")]
        public HttpResponseMessage SearchActivityProgress()
        {

            var output = UserActivityService.SearchActivityProgress();
            if (output.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting search user activity progress: Server Error");

        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/UserActivityProgress")]
        public HttpResponseMessage UserActivityProgress()
        {

            var output = UserActivityService.UserActivityProgress();
            if (output.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting  user activity progress: Server Error");

        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/UserLogin")]

        public HttpResponseMessage UserLogin()
        {

            var output = LoginService.GetUserLoginInfo();
            if (output.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting login info list: Server Error");

        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchUserLogIn/{text}")]
        public HttpResponseMessage SearchUserLogIn(string text)
        {
            if (text != null)
            {
                var output = LoginService.SearchLoginInfoList(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user login: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user login: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadUserLogInAdmin")]
        public HttpResponseMessage DownloadUserLogInAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadUserLogIn_Admin();
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Server error");
            
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchUserList/{text}")]
        public HttpResponseMessage SearchUserList(string text)
        {
            if (text != null)
            {
                var output = AllUserService.SearchUserList(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadUserListAdmin")]
        public HttpResponseMessage DownloadUserListAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadUserList_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [Route("api/admin/getallusersdetails")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage GetAllUserDetails()
        {
            var data = UserDetailsService.GetUserDetails();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting user details list : Server error");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchAllUserDetails/{text}")]
        public HttpResponseMessage SearchAllUserDetails(string text)
        {
            if (text != null)
            {
                var output = UserDetailsService.SearchAllUserDetails(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search user data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadAllUserDetailsAdmin")]
        public HttpResponseMessage DownloadAllUserDetailsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadAllUserDetails_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }
        [Route("api/admin/GetAllReqList")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage GetAllReqList()
        {
            var data = MessageRequestService.GetAllReqList();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting message request list : Server error");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchMessageRequests/{text}")]
        public HttpResponseMessage SearchMessageRequests(string text)
        {
            if (text != null)
            {
                var output = MessageRequestService.SearchMessageRequests(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search message request data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search message request  data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadMessageRequestsAdmin")]
        public HttpResponseMessage DownloadMessageRequestsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadMessageRequests_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [Route("api/admin/ChatSessions")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage ChatSessions()
        {
            var data = ChatBoxService.ChatSessions();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting chat sessions : Server error");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchChatSessions/{text}")]
        public HttpResponseMessage SearchChatSessions(string text)
        {
            if (text != null)
            {
                var output = ChatBoxService.SearchChatSessions(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search chat session data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search chat session  data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadChatSessionAdmin")]
        public HttpResponseMessage DownloadChatSessionAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadChatSession_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [Route("api/admin/PostArchives")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage PostArchives()
        {
            var data = GeneralPostService.GetPostWithActions();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting post actions : Server error");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchPostArchives/{text}")]
        public HttpResponseMessage SearchPostArchives(string text)
        {
            if (text != null)
            {
                var output = GeneralPostService.SearchPostWithActions(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadPostArchivesAdmin")]
        public HttpResponseMessage DownloadPostArchivesAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadPostArchives_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [Route("api/admin/PendingReport")]
        [HttpGet]
        [Auth("ADMIN","UAC")]
        public HttpResponseMessage PendingReport()
        {
            var data = PostReportService.PendingReport();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting pending post report : Server error");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/SearchPendingReport/{text}")]
        public HttpResponseMessage SearchPendingReport(string text)
        {
            if (text != null)
            {
                var output = PostReportService.SearchPendingReport(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search pending report data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search pending report data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/DownloadPendingReportsAdmin")]
        public HttpResponseMessage DownloadPendingReportsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadPendingReports_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }


        [Route("api/admin/InvestigatingReport")]
        [HttpGet]
        [Auth("ADMIN","UAC")]
        public HttpResponseMessage InvestigatingReport()
        {
            var data = PostReportService.InvestigatingReport();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting investigating post report : Server error");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/SearchInvestigatingReport/{text}")]
        public HttpResponseMessage SearchInvestigatingReport(string text)
        {
            if (text != null)
            {
                var output = PostReportService.SearchInvestigatingReport(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search investigating report data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search investigating report data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/DownloadInvestigatingReportsAdmin")]
        public HttpResponseMessage DownloadInvestigatingReportsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadInvestigatingReports_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchUserController/{text}")]
        public HttpResponseMessage SearchUserController(string text)
        {
            if (text != null)
            {
                var output = UACService.SearchUserController(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search uac data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search uac data: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadUserControllerAdmin")]
        public HttpResponseMessage DownloadUserControllerAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadUserController_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }
        [Route("api/admin/ClosedReport")]
        [HttpGet]
        [Auth("ADMIN","UAC")]
        public HttpResponseMessage ClosedReport()
        {
            var data = PostReportService.ClosedReport();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting closed post report : Server error");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/SearchClosedReport/{text}")]
        public HttpResponseMessage SearchClosedReport(string text)
        {
            if (text != null)
            {
                var output = PostReportService.SearchClosedReport(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search closed report data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search closed report data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/DownloadClosedReportsAdmin")]
        public HttpResponseMessage DownloadClosedReportsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadClosedReports_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [Route("api/admin/PendingContact")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage PendingContact()
        {
            var data = ContactService.PendingContact();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting pending contacts : Server error");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchPendingContacts/{text}")]
        public HttpResponseMessage SearchPendingContacts(string text)
        {
            if (text != null)
            {
                var output = ContactService.SearchPendingContacts(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search pending contacts data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search pending contacts data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadPendingContactsAdmin")]
        public HttpResponseMessage DownloadPendingContactsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadPendingContacts_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [HttpPost]
        [Auth("ADMIN")]
        [Route("api/admin/ContactSolve")]
        public HttpResponseMessage ContactSolve(ContactModel editModel)
        {

            if (editModel != null && editModel.STATUS != null)
            {
                var output = ContactService.UpdateContactStatus(editModel);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Contact issue solved sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during solving contact issue: Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during solving contact issue : Missing or not valid json data");

        }

        [Route("api/admin/SolvedContact")]
        [HttpGet]
        [Auth("ADMIN")]
        public HttpResponseMessage SolvedContact()
        {
            var data = ContactService.SolvedContact();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting solved contact issue : Server error");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/SearchSolvedContacts/{text}")]
        public HttpResponseMessage SearchSolvedContacts(string text)
        {
            if (text != null)
            {
                var output = ContactService.SearchSolvedContacts(text);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search solved contacts issue data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search solved contacts issue data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("ADMIN")]
        [Route("api/admin/DownloadSolvedContactsAdmin")]
        public HttpResponseMessage DownloadSolvedContactsAdmin()
        {

            var output = ExcelInfoDownloadService.DownloadSolvedContacts_Admin();
            if (output != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    output);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error getting excel data : Server error");

        }

        [HttpPost]
        [Auth("ADMIN","UAC")]
        [Route("api/admin/RevokeReport")]
        public HttpResponseMessage RevokeReport(PostReportModelWithID reportModel)
        {
            if (reportModel != null  && reportModel.POST_ID != 0)
            {
                var output = PostReportService.RevokePostReport(reportModel.POST_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Post Report Status Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error revoking post report : Data provided is not valid");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during revoking post report : Missing or not valid json data");


        }
    }
}

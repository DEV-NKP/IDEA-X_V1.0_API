using AppApiLayer_IDEA_X.Auth;
using BLL_IDEA_X.Helper_Classes;
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
    
    public class UserController : ApiController
    {
        //TODO -- done testing
        [Route("api/user/VerifyUserPass")]
        [HttpPost]
        [Auth("USER", "ADMIN", "UAC")]
        public HttpResponseMessage VerifyUserPass(AllUserModel model)
        {
            if (model != null && model.USERNAME != null && model.PASSWORD != null)
            {
                var data = AllUserService.VerifyUserPass(model);

                if (data != false)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 
                        "Password verified");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during password verification : Valid data not given");

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during password verification : Missing or not valid json data");
        }

        

        [Route("api/user/getprofilepic/{name}")]
        [HttpGet]
        [Auth("USER", "ADMIN", "UAC")]
        public HttpResponseMessage GetProfilePic(string name)
        {
            var data = ImageService.GetProfilePicture(name);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Profile picture not found");
        }

        [Route("api/user/getpostimg/{id}")]
        [HttpGet]
        [Auth("USER", "ADMIN", "UAC")]
        public HttpResponseMessage GetPostImage(int id)
        {
            var data = ImageService.GetPostImage(id);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Post image not found");
        }
        //TODO -- done testing
        [Route("api/user/PostActionRequest")]
        [HttpPost]
        [Auth("USER","ADMIN","UAC")]
        public HttpResponseMessage PostActionRequest(PostVoteActionModel postVote)
        {
            if(postVote != null)
            {
                var data = PostActionService.PostVoteActionRequest(postVote);

                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during sending request : Valid data not given");
                
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during sending request : Missing or not valid json data");
        }

        [Route("api/user/getuserbyname/{name}")]
        [HttpGet]
        [Auth("USER", "ADMIN", "UAC")]
        public HttpResponseMessage GetUserByName(string name)
        {
            var data = AllUserService.GetUserByName(name);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }

        
        [Route("api/user/getallusers")]
        [HttpGet]
        [Auth("ADMIN", "UAC")]
        public HttpResponseMessage GetAllUser()
        {
            var data = AllUserService.GetAllUser();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }

        [Route("api/user/logout")]
        [HttpPost]
        [Auth("USER", "ADMIN", "UAC")]
        public HttpResponseMessage UserLogOut(LoginAuthModel loginAuth)
        {
            if(loginAuth != null)
            {
                var res = LoginService.ExpireAuth(loginAuth.TOKEN_KEY);
                if (res == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Logout sucessfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Error during Logout : Valid Data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during Logout : Missing or not valid json data");
        }

        [Route("api/user/ChangeUserPass")]
        [HttpPost]
        [Auth("USER")]
        public HttpResponseMessage ChangeUserPassword(UpdatePassModel passModel)
        {
            if (ModelState.IsValid)
            {
                var output = AllUserService.UpdateUserPassword(passModel);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Password Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during updating password : Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during updating password : Missing or not valid json data");
        }
        [HttpPost]
        [Route("api/user/DeleteProfileCred")]
        [Auth("USER")]
        public HttpResponseMessage DeleteProfileCredentials(AllUserModel allUser)
        {

            var output = AllUserService.RemoveUserInfo(allUser.USERNAME);
            if (output)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                 "Profile Credentials Deleted Sucessfully");

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting profile credentials : Missing or not valid json data");

        }

        [HttpGet]
        [Route("api/user/GetUserDetailsByName/{uname}")]
        [Auth("USER")]
        public HttpResponseMessage GetUserDetailsByName(string uname)
        {
            var data = UserDetailsService.GetUserDetails(uname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }

        [HttpPost]
        [Route("api/user/UpdateProfile")]
        [Auth("USER")]
        public HttpResponseMessage UpdateProfile(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var output = UserDetailsService.UpdateUserDetails(user);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Profile Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during updating profile : Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during updating profile : Missing or not valid json data");
        }

        [HttpPost]
        [Route("api/user/DeleteProfile")]
        [Auth("USER")]
        public HttpResponseMessage DeleteProfile(AllUserModel allUser)
        {

            if (allUser != null)
            {
                var output = UserDetailsService.RemoveUserDetails(allUser.USERNAME);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Profile info Deleted Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during updating profile : Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting profile info : Missing or not valid json data");

        }
        [HttpGet]
        [Route("api/user/GetAllNotes")]
        [Auth("USER")]
        public HttpResponseMessage GetAllNotes()
        {
            var data = NotesService.GetAllNotes();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error Getting notes list : error occured in application");
        }

        [HttpGet]
        [Route("api/user/GetNotes/{username}/{date}")]
        [Auth("USER")]
        public HttpResponseMessage GetNotes(string username, string date)
        {
            var data = NotesService.GetNotesByUserAndDate(username, date);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Note not found");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/CreateNewNote")]
        public HttpResponseMessage CreateNewNote(NoteModel n)
        {
            if (ModelState.IsValid)
            {
                var output = NotesService.AddNewNote(n);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "New note added sucessfully");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error addign note: Missing or not valid json data");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdateNote")]
        public HttpResponseMessage UpdateNote(NoteModel n)
        {
            if (n != null)
            {
                var output = NotesService.UpdateNote(n);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Notes Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during updating note : Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during updating note : Missing or not valid json data");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/DeleteNote")]
        public HttpResponseMessage DeleteNote(NoteModel n)
        {

            if (n != null)
            {
                var output = NotesService.RemoveNote(n.NOTE_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Note Deleted Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting note : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting note : Missing or not valid json data");

        }
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/GetPostWithProfileImage/{uname}")]
        public HttpResponseMessage GetPostWithProfileImage(string uname)
        {
            var data = GeneralPostService.GetPostWithProfileImage(uname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                "Error getting post list : Application error");
        }

        [HttpGet]
        [Auth("USER")]
        [Route("api/user/UserActivity/{uname}")]
        public HttpResponseMessage UserActivity(string uname)
        {
            var data = GeneralPostService.GetUserPostActivity(uname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                "Error getting post activity : Application error");
        }
        //TODO -- done testing
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/CreateNewPost")]
        public HttpResponseMessage CreateNewPost(GeneralPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                var output = GeneralPostService.CreatePost(postModel);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "New post created sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating post: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating post: Missing or not valid json data");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemovePost")]
        public HttpResponseMessage RemovePost(GeneralPostModel postModel)
        {

            if (postModel != null)
            {
                var output = GeneralPostService.RemovePost(postModel.POST_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "POST Deleted Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting post : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting post : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdatePost")]
        public HttpResponseMessage UpdatePost(GeneralPostEditModel postEditModel)
        {

            var output = GeneralPostService.EditPost(postEditModel);
            if (output)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Post Updated Sucessfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during post : Missing or not valid json data");


        }

        [HttpGet]
        [Auth("USER")]
        [Route("api/user/GetPostActionList/{name}")]
        public HttpResponseMessage GetPostActionList(string name)
        {
            var data = PostActionService.GetPostActionListByUserName(name);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error getting post action list : Bad request");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/GetUserActivities/{name}")]
        public HttpResponseMessage GetUserActivities(string name)
        {
            var data = UserActivityService.GetUserActivities(name);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error getting user activities : Bad request");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/Upvote")]
        public HttpResponseMessage Upvote(PostActionModel actionModel)
        {
            if (ModelState.IsValid)
            {
                var output = PostActionService.SendUpvotePostAction(actionModel);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Post upvoted sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error upvoting post: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating post: Missing or not valid json data");
        }

        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemovePostAction")]
        public HttpResponseMessage RemovePostAction(PostActionModel actionModel)
        {

            if (actionModel != null && actionModel.POST_ACTION_ID != 0)
            {
                var output = PostActionService
                    .RemovePostAction((int)actionModel.POST_ACTION_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "POST Action Deleted Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting post action: Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting post action: Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdatePostAction")]
        public HttpResponseMessage UpdatePostAction(PostActionModel actionModel)
        {
            if (actionModel.POST_ACTION_ID != 0)
            {
                var output = PostActionService.UpdatePostAction(actionModel);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 
                        "Post Action Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error updating post action: Data provided is not valid");
            }
                
            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during updating post action : Missing or not valid json data");


        }
        [HttpGet]
        [Auth("ADMIN","UAC")]
        [Route("api/user/GetAllPostReport")]
        public HttpResponseMessage GetAllPostReport()
        {
            var data = PostReportService.GetAllPostReport();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error Getting post report list : error occured in application");
        }

        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemovePostReport")]
        public HttpResponseMessage RemovePostReport(PostReportModel reportModel)
        {

            if (reportModel != null)
            {
                var output = PostReportService.RemovePostReport(reportModel.POST_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Post Report removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing post report : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing post report : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/ReportPost")]
        public HttpResponseMessage ReportPost(PostReportModel reportModel)
        {
            if (ModelState.IsValid)
            {
                var output = PostReportService.ReportPost(reportModel);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 
                        "Post reported sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error reporting post: Valid data not given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error reporting post: Missing or not valid json data");
        }
        [HttpPost]
        [Auth("ADMIN","UAC")]
        [Route("api/user/ChangeReportStatus")]
        public HttpResponseMessage ChangeReportStatus(PostReportModelWithID reportModel)
        {
            if (reportModel != null && 
                reportModel.REPORT_STATUS != null && reportModel.REPORT_ID != 0)
            {
                var output = PostReportService.UpdateReportStatus(reportModel.REPORT_ID
                    ,reportModel.REPORT_STATUS);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Post Report Status Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error updating post report status: Data provided is not valid");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during updating post report status: Missing or not valid json data");


        }
        [Route("api/user/getallcontact")]
        [HttpGet]
        [Auth("ADMIN","UAC")]
        public HttpResponseMessage GetAllContacts()
        {
            var data = ContactService.GetContacts();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, 
                "Error in getting contact list : application error");
        }

        [HttpPost]
        
        [Route("api/user/SendContactMsg")]
        public HttpResponseMessage SendContactMsg(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var output = ContactService.SendContactMessageNonReg(model);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Contact message send sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error sending contact message: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error sending contact message: Missing or not valid json data");
        }

        [HttpPost]
        [Auth("ADMIN","UAC")]
        [Route("api/user/RemoveContactMsg")]
        public HttpResponseMessage RemoveContactMsg(ContactModel contact)
        {

            if (contact != null)
            {
                var output = ContactService.DeleteContactMsg(contact.CONTACT_ID);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Post Report removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing post report : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing post report : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdateContactMsg")]
        public HttpResponseMessage UpdateContactMsg(ContactModel contact)
        {
            if (contact != null && contact.CONTACT_ID != 0)
            {
                var output = ContactService.UpdateContactMsg(contact);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Contact message Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error updating contact message: Data provided is not valid");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during updating contact message : Missing or not valid json data");


        }
        [Route("api/user/GetChatMate/{uname}/{mname}")]
        [HttpGet]
        [Auth("USER")]
        public HttpResponseMessage GetChatMate(string uname,string mname)
        {
            var data = ChatBoxService.GetChatMate(uname,mname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound,
                "Error in getting chatmate : Not found");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/AddChatSession")]
        public HttpResponseMessage AddChatSession(ChatBoxModel chatBox)
        {
            if (ModelState.IsValid)
            {
                var output = ChatBoxService.AddChatSession(chatBox);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Chat session created sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating chat session: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating chat session: Missing or not valid json data");
        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemoveChatMate")]
        public HttpResponseMessage RemoveChatMate(ChatBoxModel chatBox)
        {

            if (ModelState.IsValid)
            {
                var output = ChatBoxService.RemoveChatMate(chatBox);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Chat session removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat session : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat session : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdateChatSession")]
        public HttpResponseMessage UpdateChatSession(ChatBoxModel chatBox)
        {
            if (chatBox != null && chatBox.CHAT_SESSION != null)
            {
                var output = ChatBoxService.UpdateChatSession(chatBox);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Chat session updated sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error updating chat session : Data provided is not valid");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during updating chat session : Missing or not valid json data");


        }

        [Route("api/user/GetUserMessageList/{sess}/{uname}/{mname}")]
        [HttpGet]
        [Auth("USER")]
        public HttpResponseMessage GetUserMessageList(string sess,string uname,string mname)
        {
            var data = UserMessageService.GetChatList(sess,uname,mname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound,
                "Error in getting chatlist :Invalid session or error in application");
        }

        [HttpPost]
        [Auth("USER")]
        [Route("api/user/SendChatMessage")]
        public HttpResponseMessage SendChatMessage(UserMessageModel userMessage)
        {
            if (ModelState.IsValid)
            {
                var output = UserMessageService.SendChatMessage(userMessage);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating chat session: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error creating chat session: Missing or not valid json data");
        }

        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemoveChatMessage")]
        public HttpResponseMessage RemoveChatMessage(UserMessageModel userMessage)
        {

            if (ModelState.IsValid)
            {
                var output = UserMessageService.RemoveUserMessage(userMessage);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Chat message removed Sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat message : Data given is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat message : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/UpdateChatMessage")]
        public HttpResponseMessage UpdateChatMessage(UserMessageModel model)
        {
            if (ModelState.IsValid)
            {
                var output = UserMessageService.EditChatMessage(model);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "Chat message updated sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error updating chat message : Data provided is not valid");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
             "Error during updating chat message : Missing or not valid json data");


        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/SendChatRequest")]
        public HttpResponseMessage SendChatRequest(MessageRequestModel userMessage)
        {

            if (userMessage != null && userMessage.SENDER != null && 
                userMessage.RECEIVER != null)
            {
                var output = MessageRequestService.SendChatRequest(userMessage);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     userMessage);

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                ChatBoxService.GetEmptyChatResult());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat message : Missing or not valid json data");

        }
        
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/AcceptChatRequest")]
        public HttpResponseMessage AcceptChatRequest(MessageRequestModel userMessage)
        {

            if (userMessage != null && userMessage.SENDER != null &&
                userMessage.RECEIVER != null)
            {
                var output = MessageRequestService.AcceptChatRequest(userMessage);
                if (output != false)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "Chat request accepted sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                ChatBoxService.GetEmptyChatResult());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during accepting chat request : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/CancelChatRequest")]
        public HttpResponseMessage CancelChatRequest(MessageRequestModel userMessage)
        {

            if (userMessage != null && userMessage.SENDER != null &&
                userMessage.RECEIVER != null)
            {
                var output = MessageRequestService.CancelChatRequest(userMessage);
                if (output != false)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "chat request cancelled sucessfully");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                ChatBoxService.GetEmptyChatResult());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during accepting chat message : Missing or not valid json data");

        }
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/DeleteChatRequest")]
        public HttpResponseMessage DeleteChatRequest(MessageRequestModel userMessage)
        {

            if (userMessage != null && userMessage.SENDER != null &&
                userMessage.RECEIVER != null)
            {
                var output = MessageRequestService.DeleteChatRequest(userMessage);
                if (output != false)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "chat request deleted sucessfullly");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                ChatBoxService.GetEmptyChatResult());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during deleting chat request : Missing or not valid json data");

        }

        [HttpGet]
        [Auth("USER")]
        [Route("api/user/CheckChatRequest/{uname}/{mname}")]
        public HttpResponseMessage CheckChatRequest(string uname,string mname)
        {
            if (uname != null &&
                mname != null)
            {
                var output = MessageRequestService.CheckChatRequest(uname,mname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting chat request: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting chat request: Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/GetOldMate/{uname}")]
        public HttpResponseMessage GetOldMate(string uname)
        {
            if (uname != null)
            {
                var output = UserDetailsService.GetOldMateList(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting old mate list: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting old mate list: Missing or not valid json data");
        }

        [HttpGet]
        [Auth("USER")]
        [Route("api/user/SearchPost/{text}")]
        public HttpResponseMessage SearchPost(string text)
        {
            if (text != null && text.Length > 0)
            {
                var output = GeneralPostService.SearchPost(text);
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

        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/SearchPost/{uname}/{id}")]
        public HttpResponseMessage SearchPost(string uname,int id)
        {
            if (uname != null && id != 0)
            {
                var output = GeneralPostService.SearchPostView(uname, id);
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
        [Auth("USER","ADMIN","UAC")]
        [Route("api/user/ViewProfile/{uname}")]
        public HttpResponseMessage ViewProfile(string uname)
        {
            
                var output = UserDetailsService.GetUserDetails(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting search post data: Internal Error");
     
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER","ADMIN")]
        [Route("api/user/GetPostReportedByName/{uname}/{id}")]
        public HttpResponseMessage GetPostReportedByName(string uname, int id)
        {
            if (uname != null && id != 0)
            {
                var output = PostReportService.GetPostReportwithIDByUname(id,uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting post report data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting post report data: Missing or not valid json data");
        }
        [HttpGet]
        [Auth("USER", "ADMIN")]
        [Route("api/user/GetPostReportedByName/{uname}")]
        public HttpResponseMessage GetPostReportedListByName(string uname)
        {
            if (uname != null )
            {
                var output = PostReportService.GetPostReportListByReporter(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting post report data: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting post report data: Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/DownloadUserLoginInfo/{uname}")]
        public HttpResponseMessage DownloadUserLoginInfo(string uname)
        {
            if (uname != null )
            {
                var output = ExcelInfoDownloadService.DownloadUserLoginInfo(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/DownloadUserPostInfo/{uname}")]
        public HttpResponseMessage DownloadUserPostInfo(string uname)
        {
            if (uname != null)
            {
                var output = ExcelInfoDownloadService.DownloadUserPostInfo(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/DownloadUserReportInfo/{uname}")]
        public HttpResponseMessage DownloadUserReportInfo(string uname)
        {
            if (uname != null)
            {
                var output = ExcelInfoDownloadService.DownloadUserReportInfo(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting excel data : Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpPost]
        [Auth("USER")]
        [Route("api/user/RemoveChat")]
        public HttpResponseMessage RemoveChat(UserMessageModel userMessage)
        {

            if (userMessage != null && userMessage.SENDER != null &&
                userMessage.RECEIVER != null)
            {
                var output = UserMessageService.RemoveChat(userMessage);
                if (output != false)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     "chat data deleted sucessfullly");

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Error during removing chat data : Data provided is not valid"
                );
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during removing chat data : Missing or not valid json data");

        }

        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/IdeaTracker/{uname}")]
        public HttpResponseMessage IdeaTracker(string uname)
        {
            if (uname != null)
            {
                var output = NotesService.IdeaTracker(uname);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting note data : Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting note data : Missing or not valid json data");
        }
        //TODO -- done testing
        [HttpGet]
        [Auth("USER")]
        [Route("api/user/IdeaTracker/{uname}/{date}")]
        public HttpResponseMessage SearchNote(string uname,string date)
        {
            if (uname != null)
            {
                var output = NotesService.SearchNote(uname,date);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting note data : Data provided is not valid or note not found");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error getting note data : Missing or not valid json data");
        }


    }
}

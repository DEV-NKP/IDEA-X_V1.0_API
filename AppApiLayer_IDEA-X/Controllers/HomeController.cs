using AppApiLayer_IDEA_X.Auth;
using BLL_IDEA_X.Helper_Classes;
using BLL_IDEA_X.Services;
using EntityLayer_IDEA_X.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AppApiLayer_IDEA_X.Controllers
{
    
    public class HomeController : ApiController
    {
        [Route("api/home/login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginAuthModel login)
        {
            if (ModelState.IsValid)
            {
                var response = LoginService.Authenticate(login);
                if (response != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    "Error during login : Wrong data given");
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "Error during login : Missing or not valid json data");
        }
        [Route("api/home/ForgotPassChange")]
        [HttpPost]
        public HttpResponseMessage ForgotPassChange(AllUserModel model)
        {
            if (model != null && model.EMAIL != null 
                && model.PASSWORD !=null)
            {
                var response = ForgotPassService.ForgotPassRequest(model);
                if (response != false)
                {
                   
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    "Error during changing password : Wrong data given");
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "Error during changing password : Missing or not valid json data");
        }
        [Route("api/home/GetAllLoginInfo")]
        [HttpGet]
        [Auth("ADMIN","UAC")]
        public HttpResponseMessage GetAllLoginInfo()
        {
            var data = LoginService.GetUserLoginInfo();
            if(data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, 
                "Error Getting login info : error occured in application");
        }


        [Route("api/home/removeLoginInfo")]
        [HttpPost]
        public HttpResponseMessage RemoveLoginInfo(AllUserModel allUser)
        {
            if(allUser != null)
            {
                var res = LoginService.RemoveLoginInfo(allUser.USERNAME);
                if (res == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         "Login info deleted sucessfully");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                "Error during removing login information : Missing or not valid json data");
        }



        [Route("api/home/AddUserDetails")]
        [HttpPost]
        public HttpResponseMessage AddUserDetails(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var output = UserDetailsService.AddUserDetails(user);
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User added sucessfully");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during adding user details : Missing or not valid json data");
        }

        [Route("api/home/SignUp")]
        [HttpPost]
        public HttpResponseMessage SignUp(UserModel u)
        {
            if (ModelState.IsValid)
            {
                var output = SignUpService.SignUp(u);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Sign Up Sucessfull");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest,
                     "Error during signing up : Invalid data given");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during signing up : Missing or not valid json data");
        }

        [Route("api/home/AddUserCredentials")]
        [HttpPost]
        public HttpResponseMessage AddUserCredentials(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var output = AllUserService.AddUserCredentials(user,"USER");
                if (output == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User added sucessfully");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during adding user credentials : Missing or not valid json data");
        }

        [Route("api/home/SendVerificationcode")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendVerificationcode(AllUserModel allUser)
        {
           if(allUser != null && allUser.EMAIL != null)
            {
                var code = CodeGenerator.GenerateVerificationCode(5);
                var res = await MailService.SendMail(allUser.EMAIL,code);
                var obj = new { msg = code,ret=true };
                if (res)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,obj,
                        JsonMediaTypeFormatter.DefaultMediaType);
                }
                return Request.CreateResponse(HttpStatusCode.Redirect, "Waiting for response");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing or invalid json data");
        }
        [Route("api/home/ValidateMailAndUserName")]
        [HttpPost]
        public HttpResponseMessage ValidateMailAndUserName(AllUserModel model)
        {

            var data = (AllUserService.UserExist(model.EMAIL) || 
                AllUserService.UserExist(model.USERNAME));
            if (data == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new { msg = "User with email already exist" });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 new { msg = "" });



        }

        [HttpPost]
        [Route("api/home/SendContactMsgNonReg")]
        public HttpResponseMessage SendContactMsgNonReg(ContactModel model)
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
        [Route("api/home/SendContactMsgReg")]
        public HttpResponseMessage SendContactMsgReg(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var output = ContactService.SendContactReg(model);
                if (output != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        output);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error sending contact message: Data provided is not valid");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error sending contact message: Missing or not valid json data");
        }

        [Route("api/home/GetUserLoginInfo/{key}")]
        [HttpGet]
        [Auth("USER")]
        public HttpResponseMessage GetUserLoginInfo(string key)
        {
            var data = LoginService.CheckAuthToken(key);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error Getting login info : error occured in application");
        }
    }


}

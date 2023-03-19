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
    
    public class UACController : ApiController
    {
        [HttpPost]
        [Auth("UAC")]
        [Route("api/uac/ChangeUACPass")]
        public HttpResponseMessage ChangeUACPass(UpdatePassModel passModel)
        {
            if (passModel != null)
            {

                var output = UACService.ChangeUACPass(passModel);
                if (output)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        "User controller Password Updated Sucessfully");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                "Error during updating user controller password : Valid data not given");

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                 "Error during updating user controller password : Missing or not valid json data");
        }

        [HttpGet]
        [Auth("ADMIN", "UAC")]
        [Route("api/uac/GetUACInfo/{uname}")]
        public HttpResponseMessage GetUACInfo(string uname)
        {
            var data = UACService.GetUserControllerInfoByName(uname);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
        }



    }
}

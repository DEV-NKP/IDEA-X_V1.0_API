using BLL_IDEA_X.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AppApiLayer_IDEA_X.Auth
{
    public class AuthAttribute : AuthorizationFilterAttribute
    {
        string[] acess_level;
        
        public AuthAttribute(params string[] acess_level)
        {
            this.acess_level = acess_level;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization;
            if (token == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.NotFound, "Auth key not found");
            }
            else
            {
                var rs = LoginService.CheckAuthToken(token.ToString());
                if (rs == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.Unauthorized, "Auth key expired or unauthorized acess");
                }
                else
                {
                    bool flag = false,ban = false;
                    foreach (var item in acess_level)
                    {
                        if (rs.USER_LEVEL.Equals(item, StringComparison.OrdinalIgnoreCase))
                        {
                            flag = true;

                        }
                        if (rs.USER_LEVEL.Equals("BANNED"))
                        {
                            ban = true;
                        }
                    }

                    if (flag== false && ban == true)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(
                            HttpStatusCode.Unauthorized, "User is banned for some reason. Please contact the admin");
                    }
                    if (flag == false && ban == false)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(
                            HttpStatusCode.Unauthorized, "Auth key expired or unauthorized access");
                    }
                    
                }
            }
            
            
            base.OnAuthorization(actionContext);
             
        }
    }
}
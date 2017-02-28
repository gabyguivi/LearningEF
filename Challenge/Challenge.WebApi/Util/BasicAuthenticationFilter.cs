using Challenge.Model;
using Challenge.Service;
using Challenge.Service.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Challenge.WebApi.Util
{
    public class AppFilterAttribute : Attribute { }
    
        public class AppServiceFilter : System.Web.Http.Filters.ActionFilterAttribute
        {                        
            public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    string authInfo = actionContext.Request.Headers.Authorization.Parameter;
                    string decodedInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo));
                    string app_id = decodedInfo.Split(':')[0];
                    string app_secret = decodedInfo.Split(':')[1];
                    if (string.IsNullOrEmpty(app_id) || string.IsNullOrEmpty(app_secret))
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                    else
                    {                                                
                        if (Manager.ExistsApp(app_id) && Manager.GetByApp(app_id).isBlocked)
                        {
                            Manager.RenewBlockTime(app_id);
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        }
                     }                  
                  }
            }
        
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class TokenAuthAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {      
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authInfo = actionContext.Request.Headers.Authorization.Parameter;
                string token = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo));
                //token = token.Replace(":", ""); used when postman is the client
                string decodedInfo = CryptoHelper.DecryptText(token.Replace(":","")).Split('=')[0];
                string app_id = decodedInfo;
                if (!Manager.ExistsApp(app_id,token))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }                
            }            
        }
    }
}